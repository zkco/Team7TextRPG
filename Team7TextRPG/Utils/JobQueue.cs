using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.Utils
{
    public class JobQueue
    {
        Queue<Action> _jobQueue = new Queue<Action>();
        bool _flush = false;
        // 내부적으로 Queue 아이템을 삽입한다.
        public void Push(Action job)
        {
            // Job을 Queue에 삽입한다.
            _jobQueue.Enqueue(job);
            if (_flush == false)
                _flush = true;
        }
        // Job 이 있는지 확인 하고 Job을 실행한다.
        public void Flush()
        {
            // Job이 모두 처리 될때까지 무한 반복
            while (true)
            {
                // Job을 꺼내온다.
                Action? action = Pop();
                // 꺼내온 Job 이 null 이면 종료
                if (action == null) return;

                // Job이 있으면 실행
                action.Invoke();
            }
        }
        // Job을 꺼내온다.
        Action? Pop()
        {
            // Job이 없으면 Null을 반환
            if (_jobQueue.Count == 0)
            {
                _flush = false;
                return null;
            }

            // Job을 꺼내서 반환한다.
            return _jobQueue.Dequeue();
        }
    }
}
