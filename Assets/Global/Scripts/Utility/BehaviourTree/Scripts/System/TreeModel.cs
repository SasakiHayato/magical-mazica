using System.Collections.Generic;
using System;
using System.Linq;
using BehaviourTree.Data;

namespace BehaviourTree
{
    public class TreeModel
    {
        public TreeModel(List<TreeDataBase> dataList)
        {
            int id = 0;

            foreach (TreeDataBase data in dataList)
            {
                if (data.HasCondition)
                {
                    data.ID = id;
                }
                else
                {
                    data.ID = 100 + id;
                }

                id++;
            }

            _executeList = dataList.OrderBy(d => d.ID).ToList();
           
            ModelData = new ModelData();
        }

        public ModelData ModelData { get; private set; }

        List<TreeDataBase> _executeList;
        
        int _executeID;
        int _saveDataBaseID = int.MinValue;
        public bool IsTaskCall { get; private set; } = false;

        ExecuteType _executeType;

        /// <summary>
        /// TreeData���X�V����ۂɌĂ�
        /// AI�����̕ύX
        /// </summary>
        public void OnNext()
        {
            IsTaskCall = false;

            TreeDataBase dataBase = GetTreeDataBase();

            if (!CheckDataBaseID(dataBase))
            {
                _saveDataBaseID = dataBase.ID;
                _executeID = 0;
            }

            TreeData treeData = GetTreeData(dataBase);
            
            ExecuteData executeData = GetExecuteData(treeData);
            
            ModelData.SetTreeDataBase(dataBase);
            ModelData.SetTreeData(treeData);
            ModelData.SetExecuteData(executeData);

            SetExecuteType(executeData);
        }

        /// <summary>
        /// TreeDataBase�̎擾
        /// 
        /// ModelData��Null�ł���ΐV�����f�[�^������
        /// Null�łȂ���Ό��݂�ModelData��Ԃ�
        /// </summary>
        /// <returns></returns>
        TreeDataBase GetTreeDataBase()
        {
            TreeDataBase data = ModelData.TreeDataBase;
            
            if (data == null)
            {
                data = _executeList.First(e => e.IsAccess);
            }

            return data;
        }

        TreeData GetTreeData(TreeDataBase dataBase)
        {
            TreeData data = dataBase.TreeData;

            return data;
        }

        /// <summary>
        /// TreeData�̎擾
        /// 
        /// List�̂͂��߂��珇��TreeData��Ԃ�
        /// �z��O�ɂȂ����ꍇ��Null��Ԃ�
        /// </summary>
        /// <param name="treeData"></param>
        /// <returns></returns>
        ExecuteData GetExecuteData(TreeData treeData) //Note. TreeData�͕K��Null�ł͂Ȃ����Ƃ�z��B
        {
            ExecuteData data;

            try
            {
                if (treeData.TreeType == ConditionType.Selector)
                {
                    _executeID = UnityEngine.Random.Range(0, treeData.ExecuteList.Count);
                }

                data = treeData.ExecuteList[_executeID];
                _executeID++;
            }
            catch(Exception)
            {
                data = null;
                _executeID = 0;
            }

            return data;
        }

        bool CheckDataBaseID(TreeDataBase dataBase)
        {
            if (_saveDataBaseID == int.MinValue)
            {
                return false;
            }
            else
            {
                return _saveDataBaseID == dataBase.ID;
            }
        }

        /// <summary>
        /// ���s�^�C�v�̌���
        /// </summary>
        /// <param name="treeData"></param>
        void SetExecuteType(ExecuteData treeData)
        {
            if (treeData == null)
            {
                _executeType = ExecuteType.Update;
            }
            else
            {
                _executeType = treeData.TreeExecuteType;
            }
        }

        public bool CheckIsCondition(ExecuteData treeData)
        {
            bool isProcess = treeData.Condition.IsProcess;

            switch (_executeType)
            {
                case ExecuteType.Update: return isProcess;

                case ExecuteType.Task:
                    
                    if (isProcess && !IsTaskCall)
                    {
                        IsTaskCall = true;
                    }

                    return IsTaskCall;

                default: return false;
            }
        }

        /// <summary>
        /// �n���ꂽDataBase��Null�łȂ����TreeData�̏��������s��
        /// </summary>
        /// <param name="dataBase"></param>
        public void Init(TreeDataBase dataBase)
        {
            if (dataBase == null)
            {
                return;
            }
            else
            {
                dataBase.TreeData.ExecuteList.ForEach(d => d.Action.Init());

                ModelData.SetTreeDataBase(null);
                ModelData.SetTreeData(null);
                ModelData.SetExecuteData(null);

                _executeID = 0;
                _saveDataBaseID = int.MinValue;
            }
        }
    }
}
