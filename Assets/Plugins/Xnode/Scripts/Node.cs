using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace XNode {
    /// <summary>
    /// Base class for all nodes
    /// </summary>
    /// <example>
    /// Classes extending this class will be considered as valid nodes by xNode.
    /// <code>
    /// [System.Serializable]
    /// public class Adder : Node {
    ///     [Input] public float a;
    ///     [Input] public float b;
    ///     [Output] public float result;
    ///
    ///     // GetValue should be overridden to return a value for any specified output port
    ///     public override object GetValue(NodePort port) {
    ///         return a + b;
    ///     }
    /// }
    /// </code>
    /// </example>
    [Serializable]
    public abstract class Node : ScriptableObject {

        [InfoBox("$Info", InfoMessageType = InfoMessageType.Error, VisibleIf = "Display")]
        [ShowIf("Error")]
        public bool 关闭错误信息;
        [HideInInspector]
        public string Info;
        [HideInInspector]
        public bool Error = false;
        public bool Display() {
            return Error && !关闭错误信息;
        }
        /// <summary> Used by <see cref="InputAttribute"/> and <see cref="OutputAttribute"/> to determine when to display the field value associated with a <see cref="NodePort"/> </summary>
        public enum ShowBackingValue {
            /// <summary> Never show the backing value </summary>
            Never,
            /// <summary> Show the backing value only when the port does not have any active connections </summary>
            Unconnected,
            /// <summary> Always show the backing value </summary>
            Always
        }
        /// 为了避免TryGetValue的大量性能消耗，在这里用最简单的方法，最老土的方法实现查找。
        private NodePort InputValue1;
        private NodePort InputValue2;
        private NodePort InputValue3;
        private NodePort InputValue4;
        private NodePort InputValue5;
        private NodePort InputValue6;
        private NodePort InputValue7;
        private NodePort InputValue8;
        private NodePort InputValue9;
        private NodePort InputValue10;


        private NodePort OutputValue1;
        private NodePort OutputValue2;
        private NodePort OutputValue3;
        private NodePort OutputValue4;
        private NodePort OutputValue5;
        private NodePort OutputValue6;
        private NodePort OutputValue7;
        private NodePort OutputValue8;


        public enum ConnectionType {
            /// <summary> Allow multiple connections</summary>
            Multiple,
            /// <summary> always override the current connection </summary>
            Override,
        }

        /// <summary> Tells which types of input to allow </summary>
        public enum TypeConstraint {
            /// <summary> Allow all types of input</summary>
            None,
            /// <summary> Allow connections where input value type is assignable from output value type (eg. ScriptableObject --> Object)</summary>
            Inherited,
            /// <summary> Allow only similar types </summary>
            Strict,
            /// <summary> Allow connections where output value type is assignable from input value type (eg. Object --> ScriptableObject)</summary>
            InheritedInverse,
        }

#region Obsolete
        [Obsolete("Use DynamicPorts instead")]
        public IEnumerable<NodePort> InstancePorts { get { return DynamicPorts; } }

        [Obsolete("Use DynamicOutputs instead")]
        public IEnumerable<NodePort> InstanceOutputs { get { return DynamicOutputs; } }

        [Obsolete("Use DynamicInputs instead")]
        public IEnumerable<NodePort> InstanceInputs { get { return DynamicInputs; } }

        [Obsolete("Use AddDynamicInput instead")]
        public NodePort AddInstanceInput(Type type, Node.ConnectionType connectionType = Node.ConnectionType.Multiple, Node.TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null) {
            return AddDynamicInput(type, connectionType, typeConstraint, fieldName);
        }

        [Obsolete("Use AddDynamicOutput instead")]
        public NodePort AddInstanceOutput(Type type, Node.ConnectionType connectionType = Node.ConnectionType.Multiple, Node.TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null) {
            return AddDynamicOutput(type, connectionType, typeConstraint, fieldName);
        }

        [Obsolete("Use AddDynamicPort instead")]
        private NodePort AddInstancePort(Type type, NodePort.IO direction, Node.ConnectionType connectionType = Node.ConnectionType.Multiple, Node.TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null) {
            return AddDynamicPort(type, direction, connectionType, typeConstraint, fieldName);
        }

        [Obsolete("Use RemoveDynamicPort instead")]
        public void RemoveInstancePort(string fieldName) {
            RemoveDynamicPort(fieldName);
        }

        [Obsolete("Use RemoveDynamicPort instead")]
        public void RemoveInstancePort(NodePort port) {
            RemoveDynamicPort(port);
        }

        [Obsolete("Use ClearDynamicPorts instead")]
        public void ClearInstancePorts() {
            ClearDynamicPorts();
        }
#endregion

        /// <summary> Iterate over all ports on this node. </summary>
        public IEnumerable<NodePort> Ports { get { foreach (NodePort port in ports.Values) yield return port; } }
        /// <summary> Iterate over all outputs on this node. </summary>
        public IEnumerable<NodePort> Outputs { get { foreach (NodePort port in Ports) { if (port.IsOutput) yield return port; } } }
        /// <summary> Iterate over all inputs on this node. </summary>
        public IEnumerable<NodePort> Inputs { get { foreach (NodePort port in Ports) { if (port.IsInput) yield return port; } } }
        /// <summary> Iterate over all dynamic ports on this node. </summary>
        public IEnumerable<NodePort> DynamicPorts { get { foreach (NodePort port in Ports) { if (port.IsDynamic) yield return port; } } }
        /// <summary> Iterate over all dynamic outputs on this node. </summary>
        public IEnumerable<NodePort> DynamicOutputs { get { foreach (NodePort port in Ports) { if (port.IsDynamic && port.IsOutput) yield return port; } } }
        /// <summary> Iterate over all dynamic inputs on this node. </summary>
        public IEnumerable<NodePort> DynamicInputs { get { foreach (NodePort port in Ports) { if (port.IsDynamic && port.IsInput) yield return port; } } }
        /// <summary> Parent <see cref="NodeGraph"/> </summary>
        [SerializeField] public NodeGraph graph;
        /// <summary> Position on the <see cref="NodeGraph"/> </summary>
        [SerializeField] public Vector2 position;
        /// <summary> It is recommended not to modify these at hand. Instead, see <see cref="InputAttribute"/> and <see cref="OutputAttribute"/> </summary>
        [SerializeField] private NodePortDictionary ports = new NodePortDictionary();
        [HideInInspector]
        public Color TintColor = Color.white;
    
        /// <summary> Used during node instantiation to fix null/misconfigured graph during OnEnable/Init. Set it before instantiating a node. Will automatically be unset during OnEnable </summary>
        public static NodeGraph graphHotfix;
        [HideInInspector]
        public string NodeName;
        protected void OnEnable() {
            if (graphHotfix != null) graph = graphHotfix;
            graphHotfix = null;
            UpdatePorts();

            UpdateInputValue();
            UpdateOutputValue();
            Init();
        }
        public void UpdateInputValue() {
            InputValue1 = null;
            InputValue2 = null;
            InputValue3 = null;
            InputValue4 = null;
            InputValue5 = null;
            InputValue6 = null;
            InputValue7 = null;
            InputValue8 = null;
            InputValue9 = null;
            InputValue10 = null;
            List<NodePort> Ports = new List<NodePort>();
            foreach (var port in ports)
            {
                if (port.Value.IsInput)
                    Ports.Add(port.Value);

            }

            if (Ports.Count == 1)
                InputValue1 = Ports[0];

            if (Ports.Count == 2)
            {
                InputValue1 = Ports[0];
                InputValue2 = Ports[1];
            }
            if (Ports.Count == 3)
            {
                InputValue1 = Ports[0];
                InputValue2 = Ports[1];
                InputValue3 = Ports[2];
            }

            if (Ports.Count == 4)
            {
                InputValue1 = Ports[0];
                InputValue2 = Ports[1];
                InputValue3 = Ports[2];
                InputValue4 = Ports[3];

            }
            if (Ports.Count == 5)
            {
                InputValue1 = Ports[0];
                InputValue2 = Ports[1];
                InputValue3 = Ports[2];
                InputValue4 = Ports[3];
                InputValue5 = Ports[4];
            }
            if (Ports.Count == 6)
            {
                InputValue1 = Ports[0];
                InputValue2 = Ports[1];
                InputValue3 = Ports[2];
                InputValue4 = Ports[3];
                InputValue5 = Ports[4];
                InputValue6 = Ports[5];
            }
            if (Ports.Count == 7)
            {
                InputValue1 = Ports[0];
                InputValue2 = Ports[1];
                InputValue3 = Ports[2];
                InputValue4 = Ports[3];
                InputValue5 = Ports[4];
                InputValue6 = Ports[5];
                InputValue7 = Ports[6];
            }
            if (Ports.Count == 8)
            {
                InputValue1 = Ports[0];
                InputValue2 = Ports[1];
                InputValue3 = Ports[2];
                InputValue4 = Ports[3];
                InputValue5 = Ports[4];
                InputValue6 = Ports[5];
                InputValue7 = Ports[6];
                InputValue8 = Ports[7];
            }
            if (Ports.Count == 9)
            {
                InputValue1 = Ports[0];
                InputValue2 = Ports[1];
                InputValue3 = Ports[2];
                InputValue4 = Ports[3];
                InputValue5 = Ports[4];
                InputValue6 = Ports[5];
                InputValue7 = Ports[6];
                InputValue8 = Ports[7];
                InputValue9 = Ports[8];

            }
            if (Ports.Count == 10)
            {
                InputValue1 = Ports[0];
                InputValue2 = Ports[1];
                InputValue3 = Ports[2];
                InputValue4 = Ports[3];
                InputValue5 = Ports[4];
                InputValue6 = Ports[5];
                InputValue7 = Ports[6];
                InputValue8 = Ports[7];
                InputValue9 = Ports[8];
                InputValue10 = Ports[9];
            }
        }
        public void UpdateOutputValue()
        {
            List<NodePort> Ports = new List<NodePort>();
            foreach (var port in ports)
            {
                if (port.Value.IsOutput)
                    Ports.Add(port.Value);

            }

            if (Ports.Count == 1)
                OutputValue1 = Ports[0];

            if (Ports.Count == 2)
            {
                OutputValue1 = Ports[0];
                OutputValue2 = Ports[1];
            }
            if (Ports.Count == 3)
            {
                OutputValue1 = Ports[0];
                OutputValue2 = Ports[1];
                OutputValue3 = Ports[2];
            }

            if (Ports.Count == 4)
            {
                OutputValue1 = Ports[0];
                OutputValue2 = Ports[1];
                OutputValue3 = Ports[2];
                OutputValue4 = Ports[3];

            }
            if (Ports.Count == 5)
            {
                OutputValue1 = Ports[0];
                OutputValue2 = Ports[1];
                OutputValue3 = Ports[2];
                OutputValue4 = Ports[3];
                OutputValue5 = Ports[4];
            }
            if (Ports.Count == 6)
            {
                OutputValue1 = Ports[0];
                OutputValue2 = Ports[1];
                OutputValue3 = Ports[2];
                OutputValue4 = Ports[3];
                OutputValue5 = Ports[4];
                OutputValue6 = Ports[5];
            }
            if (Ports.Count == 7)
            {
                OutputValue1 = Ports[0];
                OutputValue2 = Ports[1];
                OutputValue3 = Ports[2];
                OutputValue4 = Ports[3];
                OutputValue5 = Ports[4];
                OutputValue6 = Ports[5];
                OutputValue7 = Ports[6];
            }
            if (Ports.Count == 8)
            {
                OutputValue1 = Ports[0];
                OutputValue2 = Ports[1];
                OutputValue3 = Ports[2];
                OutputValue4 = Ports[3];
                OutputValue5 = Ports[4];
                OutputValue6 = Ports[5];
                OutputValue7 = Ports[6];
                OutputValue8 = Ports[7];
            }

        }

        /// <summary> Update static ports and dynamic ports managed by DynamicPortLists to reflect class fields. This happens automatically on enable or on redrawing a dynamic port list. </summary>
        public void UpdatePorts() {
        
            NodeDataCache.UpdatePorts(this, ports);
        }

        /// <summary> Initialize node. Called on enable. </summary>
        protected virtual void Init() { }

        public virtual void FunctionDo(string PortName,List<object> param = null) { }
        public virtual void FunctionFreshDo(string PortName, object param = null) { }
        public void ConnectDo(string nodeName, List<object> param = null) {
           NodePort port = GetOutputPort(nodeName);
            NodePort connect = port.Connection;
            if (connect != null)
            {

                connect.node.FunctionDo(connect.fieldName, param);
            }
        
        }
        public void ConnectFreshDo(string nodeName,object param)
        {
            NodePort port = GetOutputPort(nodeName);
            NodePort connect = port.Connection;
            if (connect != null)
            {

                connect.node.FunctionFreshDo(connect.fieldName, param);
            }

        }
        /// <summary> Checks all connections for invalid references, and removes them. </summary>
        public void VerifyConnections() {
            foreach (NodePort port in Ports) port.VerifyConnections();
        }

#region Dynamic Ports
        /// <summary> Convenience function. </summary>
        /// <seealso cref="AddInstancePort"/>
        /// <seealso cref="AddInstanceOutput"/>
        public NodePort AddDynamicInput(Type type, Node.ConnectionType connectionType = Node.ConnectionType.Multiple, Node.TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null) {
            NodePort r = AddDynamicPort(type, NodePort.IO.Input, connectionType, typeConstraint, fieldName);
            UpdateInputValue();
            return r;
        }

        /// <summary> Convenience function. </summary>
        /// <seealso cref="AddInstancePort"/>
        /// <seealso cref="AddInstanceInput"/>
        public NodePort AddDynamicOutput(Type type, Node.ConnectionType connectionType = Node.ConnectionType.Multiple, Node.TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null)
        {
            NodePort r = AddDynamicPort(type, NodePort.IO.Output, connectionType, typeConstraint, fieldName);
            UpdateOutputValue();
            return r;
        }

        /// <summary> Add a dynamic, serialized port to this node. </summary>
        /// <seealso cref="AddDynamicInput"/>
        /// <seealso cref="AddDynamicOutput"/>
        private NodePort AddDynamicPort(Type type, NodePort.IO direction, Node.ConnectionType connectionType = Node.ConnectionType.Multiple, Node.TypeConstraint typeConstraint = TypeConstraint.None, string fieldName = null) {
            if (fieldName == null) {
                fieldName = "dynamicInput_0";
                int i = 0;
                while (HasPort(fieldName)) fieldName = "dynamicInput_" + (++i);
            } else if (HasPort(fieldName)) {
                Debug.LogWarning("Port '" + fieldName + "' already exists in " + name, this);
                return ports[fieldName];
            }
            NodePort port = new NodePort(fieldName, type, direction, connectionType, typeConstraint, this);
            ports.Add(fieldName, port);
            UpdateInputValue();
            UpdateOutputValue();
            return port;
        }

        /// <summary> Remove an dynamic port from the node </summary>
        public void RemoveDynamicPort(string fieldName) {
            NodePort dynamicPort = GetPort(fieldName);
           
            if (dynamicPort == null) throw new ArgumentException("port " + fieldName + " doesn't exist");
            RemoveDynamicPort(GetPort(fieldName));
            UpdateInputValue();
            UpdateOutputValue();
        }

        /// <summary> Remove an dynamic port from the node </summary>
        public void RemoveDynamicPort(NodePort port) {
            if (port == null) throw new ArgumentNullException("port");
            else if (port.IsStatic) throw new ArgumentException("cannot remove static port");
            port.ClearConnections();
          
            ports.Remove(port.fieldName);
            UpdateInputValue();
            UpdateOutputValue();
        }

        /// <summary> Removes all dynamic ports from the node </summary>
        [ContextMenu("删除动态节点")]
        public void ClearDynamicPorts() {
            List<NodePort> dynamicPorts = new List<NodePort>(DynamicPorts);
            foreach (NodePort port in dynamicPorts) {
                RemoveDynamicPort(port);
            }
            UpdateInputValue();
            UpdateOutputValue();
        }
#endregion

#region Ports
        /// <summary> Returns output port which matches fieldName </summary>
        public NodePort GetOutputPort(string fieldName) {
            NodePort port = GetPort(fieldName);
            if (port == null || port.direction != NodePort.IO.Output) return null;
            else return port;
        }

        /// <summary> Returns input port which matches fieldName </summary>
        public NodePort GetInputPort(string fieldName) {
            NodePort port = GetPort(fieldName);
            if (port == null || port.direction != NodePort.IO.Input) return null;
            else return port;
        }

        /// <summary> Returns port which matches fieldName </summary>
        public NodePort GetPort(string fieldName) {
            NodePort port;
            if (ports.TryGetValue(fieldName, out port)) return port;
            else return null;
        }
      
        public NodePort GetInputPortDirectlyCheckNull(string fieldName) {

            if (InputValue1 != null && String.Compare(InputValue1.fieldName ,fieldName, StringComparison.Ordinal) == 0)
                return InputValue1;
            if (InputValue2 != null && String.Compare(InputValue2.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue2;
            if (InputValue3 != null && String.Compare(InputValue3.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue3;
            if (InputValue4 != null && String.Compare(InputValue4.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue4;
            if (InputValue5 != null && String.Compare(InputValue5.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue5;
            if (InputValue6 != null && String.Compare(InputValue6.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue6;
            if (InputValue7 != null && String.Compare(InputValue7.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue7;
            if (InputValue8 != null && String.Compare(InputValue8.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue8;
            return null;
        }
        public NodePort GetInputPortDirectly(string fieldName)
        {

            if (String.Compare(InputValue1.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue1;
            if (String.Compare(InputValue2.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue2;
            if (String.Compare(InputValue3.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue3;
            if (String.Compare(InputValue4.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue4;
            if (String.Compare(InputValue5.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue5;
            if (String.Compare(InputValue6.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue6;
            if (String.Compare(InputValue7.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue7;
            if (String.Compare(InputValue8.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return InputValue8;
            return null;
        }
        public NodePort GetOutputPortDirectly(string fieldName)
        {

            if (String.Compare(OutputValue1.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue1;
            if (String.Compare(OutputValue2.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue2;
            if (String.Compare(OutputValue3.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue3;
            if (String.Compare(OutputValue4.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue4;
            if (String.Compare(OutputValue5.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue5;
            if (String.Compare(OutputValue6.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue6;
            if (String.Compare(OutputValue7.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue7;
            if (String.Compare(OutputValue8.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue8;
            return null;
        }
        public NodePort GetOutputPortDirectlyCheckNull(string fieldName)
        {

            if (OutputValue1  !=null && String.Compare(OutputValue1.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue1;
            if (OutputValue2 != null && String.Compare(OutputValue2.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue2;
            if (OutputValue3 != null && String.Compare(OutputValue3.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue3;
            if (OutputValue4 != null && String.Compare(OutputValue4.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue4;
            if (OutputValue5 != null && String.Compare(OutputValue5.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue5;
            if (OutputValue6 != null && String.Compare(OutputValue6.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue6;
            if (OutputValue7 != null && String.Compare(OutputValue7.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue7;
            if (OutputValue8 != null && String.Compare(OutputValue8.fieldName, fieldName, StringComparison.Ordinal) == 0)
                return OutputValue8;
            return null;
        }
        public bool HasPort(string fieldName) {
            return ports.ContainsKey(fieldName);
        }
#endregion

#region Inputs/Outputs
        /// <summary> Return input value for a specified port. Returns fallback value if no ports are connected </summary>
        /// <param name="fieldName">Field name of requested input port</param>
        /// <param name="fallback">If no ports are connected, this value will be returned</param>
        public T GetInputValue<T>(string fieldName, T fallback = default(T)) {
            NodePort port = GetPort(fieldName);
            if (port != null && port.IsConnected) return port.GetInputValue<T>();
            else return fallback;
        }
        
        /// <summary> Return all input values for a specified port. Returns fallback value if no ports are connected </summary>
        /// <param name="fieldName">Field name of requested input port</param>
        /// <param name="fallback">If no ports are connected, this value will be returned</param>
        public T[] GetInputValues<T>(string fieldName, params T[] fallback) {
            NodePort port = GetPort(fieldName);
            if (port != null && port.IsConnected) return port.GetInputValues<T>();
            else return fallback;
        }
        public Delegate GetDelegate(NodePort port)
        {
            return Delegate.CreateDelegate(typeof(Func<NodePort ,object>),this, "GetValue");
        }
        
        /// <summary> Returns a value based on requested port output. Should be overridden in all derived nodes with outputs. </summary>
        /// <param name="port">The requested port.</param>
        public virtual object GetValue(NodePort port) {
          //  Debug.LogWarning("No GetValue(NodePort port) override defined for " + GetType());
            return null;
        }
        /*
        public virtual T GetValue<T>(NodePort port)
        {
            
            return default;
        }*/

        #endregion

        /// <summary> Called after a connection between two <see cref="NodePort"/>s is created </summary>
        /// <param name="from">Output</param> <param name="to">Input</param>
        public virtual void OnCreateConnection(NodePort from, NodePort to) { }

        /// <summary> Called after a connection is removed from this port </summary>
        /// <param name="port">Output or Input</param>
        public virtual void OnRemoveConnection(NodePort port) { }

        /// <summary> Disconnect everything from this node </summary>
        public void ClearConnections() {
            foreach (NodePort port in Ports) port.ClearConnections();
        }

#region Attributes
        /// <summary> Mark a serializable field as an input port. You can access this through <see cref="GetInputPort(string)"/> </summary>
        [AttributeUsage(AttributeTargets.Field)]
        public class InputAttribute : Attribute {
            public ShowBackingValue backingValue;
            public ConnectionType connectionType;
            [Obsolete("Use dynamicPortList instead")]
            public bool instancePortList { get { return dynamicPortList; } set { dynamicPortList = value; } }
            public bool dynamicPortList;
            public TypeConstraint typeConstraint;

            /// <summary> Mark a serializable field as an input port. You can access this through <see cref="GetInputPort(string)"/> </summary>
            /// <param name="backingValue">Should we display the backing value for this port as an editor field? </param>
            /// <param name="connectionType">Should we allow multiple connections? </param>
            /// <param name="typeConstraint">Constrains which input connections can be made to this port </param>
            /// <param name="dynamicPortList">If true, will display a reorderable list of inputs instead of a single port. Will automatically add and display values for lists and arrays </param>
            public InputAttribute(ShowBackingValue backingValue = ShowBackingValue.Unconnected, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, bool dynamicPortList = false) {
                this.backingValue = backingValue;
                this.connectionType = connectionType;
                this.dynamicPortList = dynamicPortList;
                this.typeConstraint = typeConstraint;
            }
        }

        /// <summary> Mark a serializable field as an output port. You can access this through <see cref="GetOutputPort(string)"/> </summary>
        [AttributeUsage(AttributeTargets.Field)]
        public class OutputAttribute : Attribute {
            public ShowBackingValue backingValue;
            public ConnectionType connectionType;
            [Obsolete("Use dynamicPortList instead")]
            public bool instancePortList { get { return dynamicPortList; } set { dynamicPortList = value; } }
            public bool dynamicPortList;
            public TypeConstraint typeConstraint;

            /// <summary> Mark a serializable field as an output port. You can access this through <see cref="GetOutputPort(string)"/> </summary>
            /// <param name="backingValue">Should we display the backing value for this port as an editor field? </param>
            /// <param name="connectionType">Should we allow multiple connections? </param>
            /// <param name="typeConstraint">Constrains which input connections can be made from this port </param>
            /// <param name="dynamicPortList">If true, will display a reorderable list of outputs instead of a single port. Will automatically add and display values for lists and arrays </param>
            public OutputAttribute(ShowBackingValue backingValue = ShowBackingValue.Never, ConnectionType connectionType = ConnectionType.Multiple, TypeConstraint typeConstraint = TypeConstraint.None, bool dynamicPortList = false) {
                this.backingValue = backingValue;
                this.connectionType = connectionType;
                this.dynamicPortList = dynamicPortList;
                this.typeConstraint = typeConstraint;
            }

            /// <summary> Mark a serializable field as an output port. You can access this through <see cref="GetOutputPort(string)"/> </summary>
            /// <param name="backingValue">Should we display the backing value for this port as an editor field? </param>
            /// <param name="connectionType">Should we allow multiple connections? </param>
            /// <param name="dynamicPortList">If true, will display a reorderable list of outputs instead of a single port. Will automatically add and display values for lists and arrays </param>
            [Obsolete("Use constructor with TypeConstraint")]
            public OutputAttribute(ShowBackingValue backingValue, ConnectionType connectionType, bool dynamicPortList) : this(backingValue, connectionType, TypeConstraint.None, dynamicPortList) { }
        }

        /// <summary> Manually supply node class with a context menu path </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class CreateNodeMenuAttribute : Attribute {
            public string menuName;
            public int order;
            /// <summary> Manually supply node class with a context menu path </summary>
            /// <param name="menuName"> Path to this node in the context menu. Null or empty hides it. </param>
            public CreateNodeMenuAttribute(string menuName) {
                this.menuName = menuName;
                this.order = 0;
            }

            /// <summary> Manually supply node class with a context menu path </summary>
            /// <param name="menuName"> Path to this node in the context menu. Null or empty hides it. </param>
            /// <param name="order"> The order by which the menu items are displayed. </param>
            public CreateNodeMenuAttribute(string menuName, int order) {
                this.menuName = menuName;
                this.order = order;
            }
        }

        /// <summary> Prevents Node of the same type to be added more than once (configurable) to a NodeGraph </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class DisallowMultipleNodesAttribute : Attribute {
            // TODO: Make inheritance work in such a way that applying [DisallowMultipleNodes(1)] to type NodeBar : Node
            //       while type NodeFoo : NodeBar exists, will let you add *either one* of these nodes, but not both.
            public int max;
            /// <summary> Prevents Node of the same type to be added more than once (configurable) to a NodeGraph </summary>
            /// <param name="max"> How many nodes to allow. Defaults to 1. </param>
            public DisallowMultipleNodesAttribute(int max = 1) {
                this.max = max;
            }
        }

        /// <summary> Specify a color for this node type </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class NodeTintAttribute : Attribute {
            public Color color;
            /// <summary> Specify a color for this node type </summary>
            /// <param name="r"> Red [0.0f .. 1.0f] </param>
            /// <param name="g"> Green [0.0f .. 1.0f] </param>
            /// <param name="b"> Blue [0.0f .. 1.0f] </param>
            public NodeTintAttribute(float r, float g, float b) {
                color = new Color(r, g, b);
            }

            /// <summary> Specify a color for this node type </summary>
            /// <param name="hex"> HEX color value </param>
            public NodeTintAttribute(string hex) {
                ColorUtility.TryParseHtmlString(hex, out color);
            }

            /// <summary> Specify a color for this node type </summary>
            /// <param name="r"> Red [0 .. 255] </param>
            /// <param name="g"> Green [0 .. 255] </param>
            /// <param name="b"> Blue [0 .. 255] </param>
            public NodeTintAttribute(byte r, byte g, byte b) {
                color = new Color32(r, g, b, byte.MaxValue);
            }
        }

        /// <summary> Specify a width for this node type </summary>
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
        public class NodeWidthAttribute : Attribute {
            public int width;
            /// <summary> Specify a width for this node type </summary>
            /// <param name="width"> Width </param>
            public NodeWidthAttribute(int width) {
                this.width = width;
            }
        }
#endregion

        [Serializable] private class NodePortDictionary : Dictionary<string, NodePort>, ISerializationCallbackReceiver {
            [SerializeField] private List<string> keys = new List<string>();
            [SerializeField] private List<NodePort> values = new List<NodePort>();

            public void OnBeforeSerialize() {
                keys.Clear();
                values.Clear();
                foreach (KeyValuePair<string, NodePort> pair in this) {
                    keys.Add(pair.Key);
                    values.Add(pair.Value);
                }
            }

            public void OnAfterDeserialize() {
                this.Clear();

                if (keys.Count != values.Count)
                    throw new System.Exception("there are " + keys.Count + " keys and " + values.Count + " values after deserialization. Make sure that both key and value types are serializable.");

                for (int i = 0; i < keys.Count; i++)
                    this.Add(keys[i], values[i]);
            }
        }
    }
}
