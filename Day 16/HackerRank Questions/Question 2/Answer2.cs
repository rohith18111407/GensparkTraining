using System;
using System.Collections.Generic;
using System.IO;

namespace Solution
{

    public class NotesStore
    {
        public Dictionary<string, List<string>> notesByState;
        private HashSet<string> validStates;

        public NotesStore()
        {
            notesByState = new Dictionary<string, List<string>>()
            {
                {"active", new List<string>()},
                {"completed", new List<string>()},
                {"others", new List<string>()}
            };
            validStates = new HashSet<string>() { "active", "completed", "others" };
        }

        public void AddNote(String state, String name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Name cannot be empty");
            }

            if (!validStates.Contains(state))
            {
                throw new Exception($"Invalid state {state}");
            }
            notesByState[state].Add(name);
        }

        public List<String> GetNotes(String state)
        {
            if (!validStates.Contains(state))
            {
                throw new Exception($"Invalid state {state}");
            }
            return notesByState[state];
        }


    }

    public class Solution
    {
        public static void Main()
        {
            var notesStoreObj = new NotesStore();
            var n = int.Parse(Console.ReadLine());
            for (var i = 0; i < n; i++)
            {
                var operationInfo = Console.ReadLine().Split(' ');
                try
                {
                    if (operationInfo[0] == "AddNote")
                        notesStoreObj.AddNote(operationInfo[1], operationInfo.Length == 2 ? "" : operationInfo[2]);
                    else if (operationInfo[0] == "GetNotes")
                    {
                        var result = notesStoreObj.GetNotes(operationInfo[1]);
                        if (result.Count == 0)
                            Console.WriteLine("No Notes");
                        else
                            Console.WriteLine(string.Join(",", result));
                    }
                    else
                    {
                        Console.WriteLine("Invalid Parameter");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }
    }
}