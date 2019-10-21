using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorld();
            // The Length property is used to obtain the length of the array. 
            // Notice that Length is a read-only property:
            UsoArgs(args);
            UsoClasse();
            UsoCicli();
            UsoMetodi();
            UsoScelte();
            //UsoSQL(); //facciamo finta che funziona
            UsoStruct();
            UsoTipiBase();

            Console.ReadKey();
        }

        private static void UsoTipiBase()
        {
            // dichiarare alcune tipologie di variabili
            int myInt;
            double myDouble;
            byte myByte;
            char myChar;
            decimal myDecimal;
            float myFloat;
            long myLong;
            short myShort;
            bool myBool;
            sbyte mySbyte;
            uint myUint;
            ulong myUlong;
            ushort myUshort;

            // con sizeof calcolo il numero di byte utilizzati
            myInt = 5000;
            Console.WriteLine("Integer");
            Console.WriteLine(myInt);
            Console.WriteLine(myInt.GetType());
            Console.WriteLine(sizeof(int));
            Console.WriteLine();

            myDouble = 5000.0;
            Console.WriteLine("Double");
            Console.WriteLine(myDouble);
            Console.WriteLine(myDouble.GetType());
            Console.WriteLine(sizeof(double));
            Console.WriteLine();

            myByte = 254;
            Console.WriteLine("Byte");
            Console.WriteLine(myByte);
            Console.WriteLine(myByte.GetType());
            Console.WriteLine(sizeof(byte));
            Console.WriteLine();

            myChar = 'r';
            Console.WriteLine("Char");
            Console.WriteLine(myChar);
            Console.WriteLine(myChar.GetType());
            Console.WriteLine(sizeof(byte));
            Console.WriteLine();

            myDecimal = 20987.89756M;
            Console.WriteLine("Decimal");
            Console.WriteLine(myDecimal);
            Console.WriteLine(myDecimal.GetType());
            Console.WriteLine(sizeof(byte));
            Console.WriteLine();

            myFloat = 254.09F;
            Console.WriteLine("Float");
            Console.WriteLine(myFloat);
            Console.WriteLine(myFloat.GetType());
            Console.WriteLine(sizeof(byte));
            Console.WriteLine();

            myLong = 2544567538754;
            Console.WriteLine("Long");
            Console.WriteLine(myLong);
            Console.WriteLine(myLong.GetType());
            Console.WriteLine(sizeof(byte));
            Console.WriteLine();

            myBool = true;
            Console.WriteLine("Boolean");
            Console.WriteLine(myBool);
            Console.WriteLine(myBool.GetType());
            Console.WriteLine(sizeof(byte));
            Console.WriteLine();

            mySbyte = -108;
            Console.WriteLine("sByte");
            Console.WriteLine(mySbyte);
            Console.WriteLine(mySbyte.GetType());
            Console.WriteLine(sizeof(sbyte));
            Console.WriteLine();

            myShort = -10886;
            Console.WriteLine("Short");
            Console.WriteLine(myShort);
            Console.WriteLine(myShort.GetType());
            Console.WriteLine(sizeof(short));
            Console.WriteLine();

            myUint = 4137283472;
            Console.WriteLine("uint");
            Console.WriteLine(myUint);
            Console.WriteLine(myUint.GetType());
            Console.WriteLine(sizeof(uint));
            Console.WriteLine();

            myUlong = 18446744073709551614;
            Console.WriteLine("ulong");
            Console.WriteLine(myUlong);
            Console.WriteLine(myUlong.GetType());
            Console.WriteLine(sizeof(ulong));
            Console.WriteLine();

            myUshort = 6553;
            Console.WriteLine("ushort");
            Console.WriteLine(myUshort);
            Console.WriteLine(myUshort.GetType());
            Console.WriteLine(sizeof(ushort));
            Console.WriteLine();
        }

        private static void UsoStruct()
        {
            Book myBook = new Book("MCSD Certification Toolkit", "Certificazione", "Covaci, Tiberiu", 648, 0, 81118612095, "Copertina morbida");
            Console.WriteLine(myBook.titolo);
            Console.WriteLine(myBook.categoria);
            Console.WriteLine(myBook.autore);
            Console.WriteLine(myBook.pagine);
            Console.WriteLine(myBook.attuale);
            Console.WriteLine(myBook.ISBN);
            Console.WriteLine(myBook.copertina);
            myBook.nextPage();
            myBook.prevPage();
        }

        public static void UsoSQL()
        {
            Console.WriteLine("Prova connessione al db");

            /** nel caso si utilizzino SQL credentials
                    Console.WriteLine("Inserire: UserId, Password, Nome del database, Nome del server");
                    //Crea la stringa di connessione con i valori via via letti da tastiera
                    string strConnection = "user id=" + Console.ReadLine() + ";password=" + Console.ReadLine() + ";";
                    strConnection += "database=" + Console.ReadLine() + ";server=" + Console.ReadLine();
                    //Per un controllo scrive a schermo la stinga di connessione
                    Console.WriteLine(strConnection);
                    //Istanzia SqlConnection
                    SqlConnection conn = new SqlConnection(strConnection);
            **/

            // Per prima cosa si crea una connessione "connDB" 
            // mediante SqlConnection con i dati del server
            // al quale si desidera accedere. Il nome del server è (localdb)\MSSQLLocalDB,
            // il database che contiente la tabella è esercizi
            SqlConnection connDB = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB; Integrated Security=SSPI; Initial Catalog=esercizi");

            Console.WriteLine("Prima query");
            SqlCommand query = new SqlCommand("INSERT INTO eser1 (id, contatore, username) VALUES ('1', '1', 'Riccardo'), ('2', '2', 'Marco'), ('3', '3', 'Matteo')", connDB);
            // Ora bisogna creare il comando cmd mediante SqlCommand.
            // SELECT * per prelevare tutte le colonne dalla tabella
            Console.WriteLine("Seconda query");
            SqlCommand cmd = new SqlCommand("SELECT * FROM eser1", connDB);
            try
            {
                // La connessione era solo impostata, ora la si apre
                Console.WriteLine("Connessione al DB");
                connDB.Open();
                // Si utilizza la classe DataReader per leggere la
                // tabella un record per volta, e via via stamparne
                // il contenuto sulla console
                Console.WriteLine("connessione riuscita, eseguo i comandi");
                SqlDataReader lettura = cmd.ExecuteReader();
                Console.WriteLine("ID\tcontatore\tUser name");
                Console.WriteLine("-----------------------------------------");

                // Ad ogni record letto...
                // (perchè in questo caso legge l'intera riga)
                while (lettura.Read())
                {
                    // ... estrae i valori e li stampa a schermo
                    Console.WriteLine("{0}\t{1}\t\t{2}", lettura.GetInt32(0), lettura.GetInt32(1), lettura.GetString(2));
                }
                // Chiude il DataReader		
                lettura.Close();
                // Libera le risorse utilizzate dal DataReader
                lettura.Dispose();
                // Chiude la Connessione
                connDB.Close();
                // Libera le risorse utilizzate dalla connessione
                connDB.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception Occured -->> {0}", e);
            }

            System.Threading.Thread.Sleep(8000);
        }

        private static void UsoScelte()
        {
            int first = 2;
            int second = 1;

            // singolo if che valuta due condizioni con else e riga di testo dopo
            Console.WriteLine("Utilizzo if con doppio controllo");
            if (first == 2 && second == 0)
            {
                Console.WriteLine("Output entrambe condizioni = true");
            }
            else
            {
                Console.WriteLine("Output = false, almeno una condizione non verificata");
            }
            Console.WriteLine("-------------------");
            Console.WriteLine();

            // doppio if nidificato con else e righe di testo
            Console.WriteLine("Utilizzo if con if annidato");
            if (first == 2)
            {
                if (second == 0)
                {
                    Console.WriteLine("Output seconda condizione = true");
                }
                else
                {
                    Console.WriteLine("Output seconda condizione = false");
                }
                Console.WriteLine("Output prima condizione = true");
            }
            else if (second == 0)
            {
                Console.WriteLine("Output seconda condizione = true");
            }
            else
            {
                Console.WriteLine("Output seconda condizione = false");
            }
            Console.WriteLine("Output prima condizione = false");
            Console.WriteLine("-------------------");
            Console.WriteLine();

            // utilizzo una stringa come condizione
            Console.WriteLine("utilizzo switch con stringa come condizione");
            string cond = "Buongiorno";

            switch (cond)
            {
                case "Buongiorno":
                case "Buon giorno":
                    Console.WriteLine("Buongiorno a te");
                    break;
                case "Ciao":
                case "Salve":
                    Console.WriteLine("Ciao a te");
                    break;
                case "Buonasera":
                case "Buona sera":
                    Console.WriteLine("Buonasera a te");
                    break;
                default:
                    Console.WriteLine("Non si saluta?");
                    break;
            }
        }

        private static void UsoMetodi()
        {
            Studente Studente1 = new Studente();
            Studente.Cont++;
            Studente Studente2 = new Studente();
            Studente.Cont++;
            Studente1.nome = "Riccardo";
            Studente1.cognome = "Giordano";
            Studente1.grado = "2";

            Studente2.nome = "John";
            Studente2.cognome = "Doe";
            Studente2.grado = "due";

            Studente1.display();
            Studente2.display();
        }

        private static void UsoCicli()
        {
            //utilizzo loop con for e incremento di uno
            Console.WriteLine("Incremento di uno");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            //utilizzo loop con for e decremento di uno
            Console.WriteLine("Decremento di uno");
            for (int i = 5; i > 0; i--)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            //utilizzo loop con for e incremento di due
            Console.WriteLine("Incremento di due");
            for (int i = 0; i < 10; i += 2)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            //utilizzo loop con for e incremento di multipli di 5
            Console.WriteLine("Incremento di multipli di 5");
            for (int i = 1; i <= 625; i *= 5)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine();

            //utilizzo loop con foreach e un array di numeri
            Console.WriteLine("foreach con int");
            int[] array = new int[] { 1, 2, 3, 4, 5 };
            foreach (int var in array)
            {
                Console.WriteLine(var);
            }
            Console.WriteLine();

            //utilizzo loop con foreach e array di stringhe
            Console.WriteLine("foreach con string");
            string[] arr = new string[] { "Primo", "Secondo", "Terzo", "Quarto", "Quinto" };
            foreach (string var in arr)
            {
                Console.WriteLine(var);
            }
            Console.WriteLine();

            //utilizzo loop con while
            Console.WriteLine("While incremento");
            int c = 0;
            while (c < 5)
            {
                Console.WriteLine(c);
                c++;
            }
            Console.WriteLine();

            //utilizzo loop con do-while
            Console.WriteLine("Do-While incremento");
            int d = 0;
            do
            {
                Console.WriteLine(d);
                d++;
            } while (d < 5);
        }

        private static void UsoClasse()
        {
            Console.WriteLine("-----------------------------------");
            Tokens y = new Tokens("Test divisione stringa in tokens,con separatori",
               new char[] { ' ', ',' });
            foreach (string var in y)
            {
                Console.WriteLine(var);
            }
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Premi qualsiasi tasto per terminare");
        }

        private static void UsoArgs(string[] args)
        {
            Console.WriteLine("numero di parametri = {0}", args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("Arg[{0}] = [{1}]", i, args[i]);
            }
            Console.WriteLine("premi qualsiasi tasto per terminare");
        }

        private static void HelloWorld()
        {
            Console.WriteLine("Ma ciao!");
        }

    }

    public struct Book
    {
        public string titolo;
        public string categoria;
        public string autore;
        public int pagine;
        public int attuale;
        public double ISBN;
        public string copertina;
        public Book(string titolo, string categoria, string autore, int pagine, int attuale, double isbn, string cover)
        {
            this.titolo = titolo;
            this.categoria = categoria;
            this.autore = autore;
            this.pagine = pagine;
            this.attuale = attuale;
            this.ISBN = isbn;
            this.copertina = cover;
        }
        public void nextPage()
        {
            if (attuale < pagine)
            {
                attuale++;
                Console.WriteLine("La pagina successiva è la numero: " + this.attuale);
            }
            else
            {
                Console.WriteLine("La pagina successiva è la fine del libro.");
            }
        }
        public void prevPage()
        {
            if (attuale > 1)
            {
                attuale--;
                Console.WriteLine("La pagina precedente è la numero: " + this.attuale);
            }
            else if (attuale == 1)
            {
                Console.WriteLine("Questa è la copertina del libro, la pagina precedente non esiste.");
            }
            else
            {
                Console.WriteLine("La pagina precedente è l'inizio del libro.");
            }
        }
    }

    public class Studente
    {
        public static int Cont;
        public string nome;
        public string cognome;
        public string grado;
        public string Nome()
        {
            string Nome = this.nome + " " + this.cognome;
            return Nome;
        }
        public void display()
        {
            string compl = Nome();
            Console.WriteLine(compl);
        }
    }

    public class Tokens : IEnumerable
    {
        private string[] elemento;

        public Tokens(string source, char[] delimiters)
        {
            // Analizza la stringa in token:
            elemento = source.Split(delimiters);
        }

        // IEnumerable Interface Implementation:
        // Dichiarare il metodo GetEnumerator() per IEnumerator
        public IEnumerator GetEnumerator()
        {
            return new TokenEnumerator(this);
        }

        // Classe interna per implementare interfaccia di IEnumerator:
        private class TokenEnumerator : IEnumerator
        {
            private int posizione = -1;
            private Tokens x;

            public TokenEnumerator(Tokens t)
            {
                this.x = t;
            }

            // Dichiarare il metodo Succ richiesto da IEnumerator:
            public bool MoveNext()
            {
                if (posizione < x.elemento.Length - 1)
                {
                    posizione++;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // Dichiarare il metodo Reset richiesto da IEnumerator:
            public void Reset()
            {
                posizione = -1;
            }

            // Dichiarare la proprietà Current richiesta da IEnumerator:
            public object Current
            {
                get
                {
                    return x.elemento[posizione];
                }
            }
        }
    }
}
