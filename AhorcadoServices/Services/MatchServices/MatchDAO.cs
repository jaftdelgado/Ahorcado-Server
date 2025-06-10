using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace AhorcadoServices.Services.MatchServices
{
    public class MatchDAO
    {
        public bool CreateMatch(int player1Id, int wordId)
        {
            using (var context = new ahorcadoDBEntities())
            {
                var match = new Matches
                {
                    Player1 = player1Id,
                    Player2 = null,
                    WordID = wordId,
                    CreateDate = DateTime.Now,
                    EndDate = null,
                    StatusID = 1
                };

                context.Matches.Add(match);
                context.SaveChanges();
                return true;
            }
        }
    }
}
