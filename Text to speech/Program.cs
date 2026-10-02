using System;
using System.Speech.Synthesis;
using System.IO;

namespace Text_to_speech
{
    internal class Program
    {
        // Speech synthesizer
        static SpeechSynthesizer speech = new SpeechSynthesizer();

        static void Main(string[] args)
        {
            // Basic speech settings
            speech.Volume = 100;
            speech.Rate = 0;

            Console.Title = "Text To Speech Generator";

            while (true)
            {
                Console.Clear();

                Console.WriteLine("======================================");
                Console.WriteLine("       TEXT TO SPEECH GENERATOR");
                Console.WriteLine("======================================");
                Console.WriteLine();
                Console.WriteLine("1. Speak Text");
                Console.WriteLine("2. Select Voice");
                Console.WriteLine("3. Change Speech Speed");
                Console.WriteLine("4. Change Volume");
                Console.WriteLine("5. Save Speech as WAV");
                Console.WriteLine("6. Stop Speaking");
                Console.WriteLine("7. Exit");
                Console.WriteLine();
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SpeakText();
                        break;

                    case "2":
                        SelectVoice();
                        break;

                    case "3":
                        ChangeSpeed();
                        break;

                    case "4":
                        ChangeVolume();
                        break;

                    case "5":
                        SaveSpeech();
                        break;

                    case "6":
                        StopSpeaking();
                        break;

                    case "7":
                        speech.Dispose();
                        return;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        // ==========================================
        // SPEAK TEXT
        // ==========================================
        static void SpeakText()
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("              SPEAK TEXT");
            Console.WriteLine("======================================");
            Console.WriteLine();

            Console.Write("Enter text: ");

            string text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine();
                Console.WriteLine("Please enter some text.");
                Pause();
                return;
            }

            try
            {
                Console.WriteLine();
                Console.WriteLine("Speaking...");
                Console.WriteLine();

                speech.SpeakAsyncCancelAll();
                speech.SpeakAsync(text);

                Console.WriteLine("Press ENTER to return to the menu.");

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Error:");
                Console.WriteLine(ex.Message);

                Pause();
            }
        }

        // ==========================================
        // SELECT VOICE
        // ==========================================
        static void SelectVoice()
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("             SELECT VOICE");
            Console.WriteLine("======================================");
            Console.WriteLine();

            var voices = speech.GetInstalledVoices();

            if (voices.Count == 0)
            {
                Console.WriteLine("No voices were found on this computer.");
                Pause();
                return;
            }

            for (int i = 0; i < voices.Count; i++)
            {
                Console.WriteLine(
                    $"{i + 1}. {voices[i].VoiceInfo.Name} " +
                    $"({voices[i].VoiceInfo.Culture.DisplayName})");
            }

            Console.WriteLine();
            Console.Write("Enter voice number: ");

            string input = Console.ReadLine();

            int number;

            if (int.TryParse(input, out number))
            {
                if (number >= 1 && number <= voices.Count)
                {
                    try
                    {
                        speech.SelectVoice(
                            voices[number - 1].VoiceInfo.Name);

                        Console.WriteLine();
                        Console.WriteLine(
                            "Selected voice: " +
                            voices[number - 1].VoiceInfo.Name);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid voice number.");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a number.");
            }

            Pause();
        }

        // ==========================================
        // CHANGE SPEED
        // ==========================================
        static void ChangeSpeed()
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("          CHANGE SPEECH SPEED");
            Console.WriteLine("======================================");
            Console.WriteLine();

            Console.WriteLine("Speed range: -10 to 10");
            Console.WriteLine("-10 = Very Slow");
            Console.WriteLine(" 0  = Normal");
            Console.WriteLine("10  = Very Fast");
            Console.WriteLine();

            Console.WriteLine(
                "Current speed: " + speech.Rate);

            Console.WriteLine();

            Console.Write("Enter new speed: ");

            string input = Console.ReadLine();

            int speed;

            if (int.TryParse(input, out speed))
            {
                if (speed >= -10 && speed <= 10)
                {
                    speech.Rate = speed;

                    Console.WriteLine();
                    Console.WriteLine(
                        "Speech speed changed to " + speed);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Speed must be between -10 and 10.");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a valid number.");
            }

            Pause();
        }

        // ==========================================
        // CHANGE VOLUME
        // ==========================================
        static void ChangeVolume()
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("             CHANGE VOLUME");
            Console.WriteLine("======================================");
            Console.WriteLine();

            Console.WriteLine("Volume range: 0 to 100");
            Console.WriteLine("0   = Silent");
            Console.WriteLine("100 = Maximum");
            Console.WriteLine();

            Console.WriteLine(
                "Current volume: " + speech.Volume);

            Console.WriteLine();

            Console.Write("Enter new volume: ");

            string input = Console.ReadLine();

            int volume;

            if (int.TryParse(input, out volume))
            {
                if (volume >= 0 && volume <= 100)
                {
                    speech.Volume = volume;

                    Console.WriteLine();
                    Console.WriteLine(
                        "Volume changed to " + volume);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(
                        "Volume must be between 0 and 100.");
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a valid number.");
            }

            Pause();
        }

        // ==========================================
        // SAVE SPEECH AS WAV
        // ==========================================
        static void SaveSpeech()
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("          SAVE SPEECH AS WAV");
            Console.WriteLine("======================================");
            Console.WriteLine();

            Console.Write("Enter text: ");

            string text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine();
                Console.WriteLine("Please enter some text.");
                Pause();
                return;
            }

            Console.WriteLine();

            Console.Write("Enter file name (example: speech.wav): ");

            string fileName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(fileName))
            {
                Console.WriteLine();
                Console.WriteLine("Invalid file name.");
                Pause();
                return;
            }

            // Add .wav automatically
            if (!fileName.EndsWith(
                ".wav",
                StringComparison.OrdinalIgnoreCase))
            {
                fileName += ".wav";
            }

            try
            {
                string desktop =
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.Desktop);

                string filePath =
                    Path.Combine(desktop, fileName);

                speech.SetOutputToWaveFile(filePath);

                speech.Speak(text);

                // Return output to speakers
                speech.SetOutputToDefaultAudioDevice();

                Console.WriteLine();
                Console.WriteLine("Speech saved successfully!");
                Console.WriteLine();
                Console.WriteLine("File location:");
                Console.WriteLine(filePath);
            }
            catch (Exception ex)
            {
                speech.SetOutputToDefaultAudioDevice();

                Console.WriteLine();
                Console.WriteLine("Could not save the file.");
                Console.WriteLine();
                Console.WriteLine("Error:");
                Console.WriteLine(ex.Message);
            }

            Pause();
        }

        // ==========================================
        // STOP SPEAKING
        // ==========================================
        static void StopSpeaking()
        {
            speech.SpeakAsyncCancelAll();

            Console.WriteLine();
            Console.WriteLine("Speech stopped.");

            Pause();
        }

        // ==========================================
        // PAUSE PROGRAM
        // ==========================================
        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}
