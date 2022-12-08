using System.Collections.Generic;
using System;
using UnityEngine;
using BehaviourTree.Data;
using BehaviourTree.IO;

namespace BehaviourTree
{
    /// <summary>
    /// BehaviorTreeを使用するObjectにアタッチするクラス
    /// AI挙動の操作を行う
    /// </summary>
    public class BehaviourTreeUser : MonoBehaviour
    {
        [SerializeField] string _userPath;
        [SerializeField] bool _runUpdate = true;
        [SerializeField] Transform _user;
        [SerializeField] int _limitConditionalCount;
        [SerializeField] List<TreeDataBase> _treeDataList;

        bool _runRequest = true;

        TreeModel _treeModel;
        ModelData ModelData => _treeModel.ModelData;

        /// <summary>
        /// 任意のタイミングでTreeModelを呼び出す
        /// </summary>
        public Action OnNext { get; private set; }

        public int UserID { get; private set; }

        void Start()
        {
            SetUserData();
            SetModelData();
            SetAction();
        }

        void SetUserData()
        {
            Transform offset = _user;

            if (offset == null)
            {
                offset = transform;
                _user = transform;
            }

            if (_userPath == "")
            {
                _userPath = BehaviourTreeMasterData.CreateUserPath();
                Debug.LogWarning($"{gameObject.name} has not UserPath. So Create it. PathName is => {_userPath}.");
            }

            UserID = BehaviourTreeMasterData.CreateUserID();

            BehaviourTreeMasterData.Instance.CreateUser(UserID, _userPath, this, offset);

            BehaviourTreeUserData userData = BehaviourTreeMasterData.Instance.FindUserData(UserID);

            for (int index = 0; index < _limitConditionalCount; index++)
            {
                userData.SetLimitConditionalData(index);
            }

            

            return; // バグが出るので、返す。要修正

            #if UNITY_EDITOR
            string ioPath;
            BehaviourTreeIO.CreateFile(_userPath, out ioPath);
            userData.SetIOPath(ioPath);
            #endif
        }

        void SetModelData()
        {
            GameObject user = _user != null ? _user.gameObject : gameObject;

            _treeDataList
                .ForEach(d => d.NodeList
                .ForEach(n =>
                {
                    n.SetNodeUser(user);
                    n.SetUp();
                }));

            _treeModel = new TreeModel(_treeDataList);
        }

        void SetAction()
        {
            OnNext += () =>
            {
                if (!HasModelDataCheck() && !_treeModel.IsTaskCall)
                {
                    Set();
                }
            };

            OnNext += () =>
            {
                if (Execute())
                {
                    switch (ModelData.TreeData.TreeType)
                    {
                        case ConditionType.Selector: Set(); break;
                        case ConditionType.Sequence: _treeModel.OnNext(); break;
                    }
                }
            };
        }

        void Update()
        {
            if (_runUpdate && _runRequest)
            {
                Run();
            }
        }

        void Run()
        {
            if (!HasModelDataCheck() && !_treeModel.IsTaskCall)
            {
                Set();
            }
            else
            {
                if (Execute())
                {
                    switch (ModelData.TreeData.TreeType)
                    {
                        case ConditionType.Selector: Set(); break;
                        case ConditionType.Sequence:

                            ModelData.ExecuteData.Action.Init();
                            _treeModel.OnNext()
                            ; break;
                    }
                }
            }
        }

        bool HasModelDataCheck()
        {
            if (ModelData.TreeDataBase == null ||
                !ModelData.TreeDataBase.IsAccess ||
                ModelData.ExecuteData == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// TreeDataの挿入
        /// </summary>
        void Set()
        {
            _treeModel.Init(ModelData.TreeDataBase);
            _treeModel.OnNext();
        }

        /// <summary>
        /// TreeDataの実行Process
        /// </summary>
        bool Execute()
        {
            if (_treeModel.CheckIsCondition(ModelData.ExecuteData))
            {
                // Note. 行動が全て終了した時点でTrueを返す
                if (ModelData.ExecuteData.Action.IsProcess)
                {
                    return true;
                }
            }
            else
            {
                return true;
            }

            return false;
        }

        public void SetRunRequest(bool isRun) => _runRequest = isRun;

        private void OnDestroy()
        {
            BehaviourTreeMasterData.Instance.DeleteUser(UserID);
        }
    }
}
