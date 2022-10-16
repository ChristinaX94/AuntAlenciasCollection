using AuntAlenciasCollection.Commons;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Result = AuntAlenciasCollection.Commons.Result;

namespace AuntAlenciasCollection.DataHandling
{
    public class StringReader
    {
        private List<string> Characters = new List<string>() { "Alencia", "Choux" };
        private List<string> Commands = new List<string>() {"show", "save", "remove" };
        private const char BotCommand = '\\';
        private const string BotCharacteristic = "aac";

        private List<string> inputSegmented;

        public Result setInputAndHandle(string input)
        {
            Result result = new Result();
            try
            {
                result = this.validateInput(input);
                if (!result.success)
                {
                    return result;
                }

                result = this.execInput();
                if (!result.success)
                {
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return result;
        }

        public Result validateInput(string input)
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    result.success = false;
                    result.message = "Empty command";
                }

                char initChar = input.ToCharArray().FirstOrDefault();
                if(initChar != BotCommand)
                {
                    result.success = false;
                    result.message = "Invalid command";
                }

                this.inputSegmented = input.Remove(initChar).Split(" ").ToList();
                
                if (!inputSegmented[0].Equals(BotCharacteristic))
                {
                    result.success = false;
                    result.message = "Invalid initiating command!";
                }

                if (!Commands.Any(x => x.Equals(inputSegmented[0])))
                {
                    result.success = false;
                    result.message = "Invalid command!";
                }

                if (!Characters.Any(x => x.Equals(inputSegmented[1])))
                {
                    result.success = false;
                    result.message = "Invalid character!";
                }

                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return result;

        }

        private Result execInput()
        {
            Result result = new Result();
            try
            {
                
            }
            catch (Exception ex)
            {
                result.success = false;
                result.message = ex.Message;
            }
            return result;
        }
    }
}
