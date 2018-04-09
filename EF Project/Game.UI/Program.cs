using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Data;
using Game.Domain;

namespace Game.UI
{
    class Program
    {
        private static GameContext _context = new GameContext();

        static void Main(string[] args)
        {
            //TournamentModification.AddTournament();
            //TournamentModification.AddTournaments();
            //TournamentModification.GetAllTournaments();
            //TournamentModification.FindTournament();
            //TournamentModification.FindFirstTournament();
            //TournamentModification.UpdateTournament();
            //TournamentModification.UpdateTournamentDisconnected();
            //TournamentModification.DeleteTournament();
            //TournamentModification.DeleteManyTournaments();
            //TournamentModification.DeleteManyTournamentsDisconnected();

            //MatchModification.AddMatch();
            //MatchModification.AddMatches();
            //MatchModification.GetAllMatches();
            //MatchModification.FindMatch();
            //MatchModification.FindFirstMatchTime();
            //MatchModification.UpdateMatch();
            //MatchModification.UpdateMatchDisconnected();
            //MatchModification.DeleteMatch();
            //MatchModification.DeleteManyMatchesFromTournament();

            //PlayerModification.AddPlayer();
            //PlayerModification.AddPlayers();
            //PlayerModification.GetAllPlayers();
            //PlayerModification.FindPlayer();
            //PlayerModification.UpdatePlayer();
            //PlayerModification.UpdatePlayerDisconnected();
            //PlayerModification.DeletePlayer();
            //PlayerModification.DeleteManyPlayers();
            //PlayerModification.DeleteManyPlayersDisconnected();

            //CharacterModification.AddCharacter();
            //CharacterModification.AddCharacters();
            //CharacterModification.GetAllCharacters();
            //CharacterModification.FindCharacter();
            //CharacterModification.UpdateCharacter();
            ////CharacterModification.UpdateCharacterDisconnected();
            //CharacterModification.DeleteCharacter();
            //CharacterModification.DeleteManyCharacters();
            //CharacterModification.DeleteManyCharactersDisconnected();

            //SpMoveModification.AddSpecialMove();
            //SpMoveModification.AddSpecialMoves();
            //SpMoveModification.GetAllSpecialMoves();
            SpMoveModification.FindSpecialMove();
            //SpMoveModification.UpdateSpecialMove();
            //SpMoveModification.UpdateSpecialMoveDisconnected();
            //SpMoveModification.DeleteSpecialMove();
            //SpMoveModification.DeleteManySpecialMoves();
            //SpMoveModification.DeleteManySpecialMovesDisconnected();
        }

    }
}
