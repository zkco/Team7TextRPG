using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Managers
{
    public class JobManager
    {
        private static JobManager? _instance = null;
        public static JobManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new JobManager();

                return _instance;
            }
        }

        private JobQueue _jobQueue = new JobQueue();

        // 외부에서 Job을 삽입할 수 있도록 하는 함수
        public void Push(Action job)
        {
            _jobQueue.Push(job);
        }
        // 외부에서 Flush를 호출할 수 있도록 하는 함수
        public void Flush()
        {
            _jobQueue.Flush();
        }
        public void Clear()
        {
            _jobQueue.Clear();
        }
    }
}
