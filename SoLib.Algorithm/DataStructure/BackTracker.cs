using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoLib.Algorithm.DataStructure
{
    public class BackTracker<T>
    {
        protected const Int32 MAXCANDIDATES = 100;
        protected Boolean _finished = false;

        public void BackTrack(T[] answers, Int32 k, T input)
        {
            if (IsSolution(answers, k, input))
            {
                ProcessSolution(answers, k, input);
            }
            else
            {
                T[] candidates = ConstructCandidates(answers, k, input);
                for (int i = 0; i < candidates.Length; i++)
                {
                    answers[k] = candidates[i];
                    MakeMove(answers, k, input);
                    BackTrack(answers, k + 1, input);
                    if (_finished)
                    {
                        return;
                    }
                    else
                    {
                        UnmakeMove(answers, k, input);
                    }
                }
            }
        }

        protected virtual void UnmakeMove(T[] a, int k, T input)
        {
            throw new NotImplementedException();
        }

        protected virtual void MakeMove(T[] a, int k, T input)
        {
            throw new NotImplementedException();
        }

        protected virtual T[] ConstructCandidates(T[] a, int k, T input)
        {
            throw new NotImplementedException();
        }

        protected virtual void ProcessSolution(T[] a, int k, T input)
        {
            throw new NotImplementedException();
        }

        protected virtual Boolean IsSolution(T[] a, int k, T input)
        {
            throw new NotImplementedException();
        }
    }

    public class SubsetsBackTracker : BackTracker<Int32>
    {
        protected override bool IsSolution(Int32[] a, Int32 k, Int32 input)
        {
            return k == input;
        }

        protected override Int32[] ConstructCandidates(Int32[] a, Int32 k, Int32 input)
        {
            return new Int32[] { 1, 0 };
        }

        protected override void ProcessSolution(Int32[] a, Int32 k, Int32 input)
        {
            Debug.Write("{");
            for (int i = 0; i < k; i++)
            {
                if (a[i] == 1)
                {
                    Debug.Write($"{i + 1}, ");
                }
            }
            Debug.Write("}");
            Debug.WriteLine("");
        }

        protected override void MakeMove(Int32[] a, Int32 k, Int32 input)
        {
        }

        protected override void UnmakeMove(Int32[] a, Int32 k, Int32 input)
        {
        }
    }

    public class PermutationBackTracker : BackTracker<Int32>
    {
        protected override bool IsSolution(Int32[] a, Int32 k, Int32 input)
        {
            return k == input;
        }

        protected override Int32[] ConstructCandidates(Int32[] a, Int32 k, Int32 input)
        {
            Boolean[] permutation = new Boolean[input];
            for (int i = 0; i < k; i++)
            {
                permutation[a[i]] = true;
            }

            Int32[] candidates = new Int32[input - k];

            Int32 index = 0;
            for (Int32 i = 0; i < input; i++)
            {
                if (!permutation[i])
                {
                    candidates[index] = i;
                    index++;
                }
            }

            return candidates;
        }

        protected override void ProcessSolution(Int32[] a, Int32 k, Int32 input)
        {
            Debug.Write("{");
            for (int i = 0; i < k; i++)
            {
                Debug.Write($"{a[i]}, ");
            }
            Debug.Write("}");
            Debug.WriteLine("");
        }

        protected override void MakeMove(int[] a, int k, int input)
        {
            
        }

        protected override void UnmakeMove(int[] a, int k, int input)
        {
            
        }
    }
}
