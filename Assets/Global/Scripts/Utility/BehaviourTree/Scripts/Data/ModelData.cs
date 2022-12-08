
namespace BehaviourTree.Data
{
    /// <summary>
    /// TreeModel�̃f�[�^�N���X�B�������Q�Ƃ���TreeModel���X�V����
    /// </summary>
    public class ModelData 
    {
        public TreeDataBase TreeDataBase { get; private set; }

        public TreeData TreeData { get; private set; }

        public ExecuteData ExecuteData { get; private set; }

        public void SetTreeDataBase(TreeDataBase data) => TreeDataBase = data;

        public void SetTreeData(TreeData data) => TreeData = data;

        public void SetExecuteData(ExecuteData data) => ExecuteData = data;
    }
}
