using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMatchingCamera : MonoBehaviour{

    public Shader fogShader;
    private Material fogMaterial = null;
    public Material material
    {
        get
        {
            //fogMaterial = CheckShaderAndCreateMaterial(fogShader, fogMaterial);
            if (fogShader == null)
            {
                return null;
            }

            if (fogShader.isSupported && fogMaterial && fogMaterial.shader == fogShader)
                return fogMaterial;

            if (!fogShader.isSupported)
            {
                return null;
            }
            else
            {
                fogMaterial = new Material(fogShader);
                fogMaterial.hideFlags = HideFlags.DontSave;
                if (fogMaterial)
                    return fogMaterial;
                else
                    return null;
            }
            //return fogMaterial;
        }
    }

    private Camera myCamera;
    public Camera camera
    {
        get
        {
            if(myCamera == null)
            {
                myCamera = transform.GetComponent<Camera>();

            }
            return myCamera;
        }
    }

    public Transform cameraTransform
    {
        get
        {
            return camera.transform;
        }
    }


    [Range(0.0f, 3.0f)]
    public float fogDensity = 1.0f;

    public Color fogColor = Color.white;

    public float fogStart = 0.0f;

    public float fogEnd = 2.0f;


    private void OnEnable()
    {
        camera.depthTextureMode |= DepthTextureMode.Depth;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(material != null)
        {
            Matrix4x4 frustumCorners = Matrix4x4.identity;

            float fov = camera.fieldOfView;
            float near = camera.nearClipPlane;
            float far = camera.farClipPlane;
            float aspect = camera.aspect;

            float halfHeight = near * Mathf.Tan(fov * 0.5f * Mathf.Deg2Rad);
            Vector3 toRight = cameraTransform.right * halfHeight * aspect;
            Vector3 toTop = cameraTransform.up * halfHeight;

            Vector3 topLeft = cameraTransform.forward * near - toRight + toTop;
            float scale = topLeft.magnitude / near;

            topLeft.Normalize();
            topLeft *= scale;

            Vector3 topRight = cameraTransform.forward * near + toTop + toRight;
            topRight.Normalize();
            topRight *= scale;

            Vector3 bottomLeft = cameraTransform.forward * near - toRight - toTop;
            bottomLeft.Normalize();
            bottomLeft *= scale;

            Vector3 bottomRight = cameraTransform.forward * near + toRight - toTop;
            bottomRight.Normalize();
            bottomRight *= scale;

            frustumCorners.SetRow(0, bottomLeft);
            frustumCorners.SetRow(1, bottomRight);
            frustumCorners.SetRow(2, topRight);
            frustumCorners.SetRow(3, topLeft);

            material.SetMatrix("_FrustumCornersRay", frustumCorners);
            material.SetMatrix("_ViewProjectionInverseMatrix", (camera.projectionMatrix * camera.worldToCameraMatrix).inverse);

            material.SetFloat("_FogDensity", fogDensity);
            material.SetColor("_FogColor", fogColor);
            material.SetFloat("_FogStart", fogStart);
            material.SetFloat("_FogEnd", fogEnd);

            Graphics.Blit(source, destination, material);

        }
        else
        {
            Graphics.Blit(source, destination);
        }
    }

}

