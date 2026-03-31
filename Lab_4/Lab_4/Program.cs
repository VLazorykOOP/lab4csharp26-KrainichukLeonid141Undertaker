using static System.Console;
namespace Lab_4
{
    internal class Program
    {

public class Date
    {
        private DateTime _date;

        public Date(int year, int month, int day)
        {
            _date = new DateTime(year, month, day);
        }

        public DateTime this[int i] => _date.AddDays(i);

        public static bool operator !(Date d)
        {
            return d._date.Day != DateTime.DaysInMonth(d._date.Year, d._date.Month);
        }

        public static bool operator true(Date d)
        {
            return d._date.Day == 1 && d._date.Month == 1;
        }

        public static bool operator false(Date d)
        {
            return !(d._date.Day == 1 && d._date.Month == 1);
        }

        public static bool operator &(Date d1, Date d2)
        {
            return d1._date == d2._date;
        }
        public static implicit operator string(Date d) => d._date.ToString("yyyy-MM-dd");
        public static explicit operator Date(string s)
        {
            DateTime dt = DateTime.Parse(s);
            return new Date(dt.Year, dt.Month, dt.Day);
        }

        public override string ToString() => (string)this;
}
        public class VectorByte
        {
            protected byte[] BArray;
            protected uint n;
            protected int codeError;
            protected static uint num_vec = 0;

            public VectorByte()
            {
                n = 1;
                BArray = new byte[n];
                BArray[0] = 0;
                num_vec++;
            }

            public VectorByte(uint size)
            {
                n = size;
                BArray = new byte[n];
                num_vec++;
            }

            public VectorByte(uint size, byte value) : this(size)
            {
                for (int i = 0; i < n; i++) BArray[i] = value;
            }

            ~VectorByte()
            {
                WriteLine("VectorByte знищено");
            }

            public uint Size => n;
            public int CodeError { get => codeError; set => codeError = value; }

            public static uint CountVectors() => num_vec;

            public byte this[int i]
            {
                get
                {
                    if (i < 0 || i >= n) { codeError = -1; return 0; }
                    return BArray[i];
                }
                set
                {
                    if (i < 0 || i >= n) codeError = -1;
                    else BArray[i] = value;
                }
            }

            public void Input()
            {
                for (int i = 0; i < n; i++)
                {
                    Write($"[{i}] = ");
                    BArray[i] = byte.Parse(ReadLine());
                }
            }

            public void Output() => Console.WriteLine(string.Join(", ", BArray));

            public static VectorByte operator +(VectorByte v1, VectorByte v2)
            {
                uint maxN = Math.Max(v1.n, v2.n);
                VectorByte res = new VectorByte(maxN);
                for (int i = 0; i < Math.Min(v1.n, v2.n); i++) res[i] = (byte)(v1[i] + v2[i]);
                return res;
            }

            public static VectorByte operator +(VectorByte v1, byte scalar)
            {
                VectorByte res = new VectorByte(v1.n);
                for (int i = 0; i < v1.n; i++) res[i] = (byte)(v1[i] + scalar);
                return res;
            }

            public static bool operator ==(VectorByte v1, VectorByte v2)
            {
                if (v1.n != v2.n) return false;
                for (int i = 0; i < v1.n; i++) if (v1[i] != v2[i]) return false;
                return true;
            }
            public static bool operator !=(VectorByte v1, VectorByte v2) => !(v1 == v2);

            public static bool operator true(VectorByte v)
            {
                if (v.n == 0) return false;
                foreach (var b in v.BArray) if (b != 0) return true;
                return false;
            }
            public static bool operator false(VectorByte v) => !(v ? true : false);

            public static VectorByte operator ++(VectorByte v)
            {
                for (int i = 0; i < v.n; i++)
                {
                    v.BArray[i]++;
                }
                return v;
            }

            public static VectorByte operator --(VectorByte v)
            {
                for (int i = 0; i < v.n; i++)
                {
                    v.BArray[i]--;
                }
                return v;
            }
        }
        public class MatrixByte
        {
            protected byte[,] ByteArray;
            protected uint n, m;
            protected int codeError;
            protected static int num_mat = 0;

            public MatrixByte() { n = m = 1; ByteArray = new byte[n, m]; num_mat++; }

            public MatrixByte(uint rows, uint cols)
            {
                n = rows; m = cols;
                ByteArray = new byte[n, m];
                num_mat++;
            }

            public MatrixByte(uint rows, uint cols, byte val) : this(rows, cols)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++) ByteArray[i, j] = val;
            }

            public byte this[int i, int j]
            {
                get
                {
                    if (i < 0 || i >= n || j < 0 || j >= m) { codeError = -1; return 0; }
                    return ByteArray[i, j];
                }
                set
                {
                    if (i < 0 || i >= n || j < 0 || j >= m) codeError = -1;
                    else ByteArray[i, j] = value;
                }
            }

            public byte this[int k]
            {
                get => this[k / (int)m, k % (int)m];
                set => this[k / (int)m, k % (int)m] = value;
            }

            public static VectorByte operator *(MatrixByte mat, VectorByte vec)
            {
                VectorByte res = new VectorByte(mat.n);
                for (int i = 0; i < mat.n; i++)
                {
                    int sum = 0;
                    for (int j = 0; j < Math.Min(mat.m, vec.Size); j++)
                        sum += mat[i, j] * vec[j];
                    res[i] = (byte)sum;
                }
                return res;
            }

            public static MatrixByte operator ++(MatrixByte mat)
            {
                for (int i = 0; i < mat.n; i++)
                    for (int j = 0; j < mat.m; j++) mat.ByteArray[i, j]++;
                return mat;
            }

            public static bool operator !(MatrixByte mat) => mat.n != 0 && mat.m != 0;

            public void Output()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++) Write(ByteArray[i, j] + "\t");
                    WriteLine();
                }
            }
        }
        static void Main(string[] args)
        {
            OutputEncoding = System.Text.Encoding.UTF8;

            // --- Тестування Завдання 1: Клас Date ---
            WriteLine("=== ТЕСТУВАННЯ КЛАСУ Date ===");
            Date today = new Date(2023, 12, 31);
            Date JanFirst = new Date(2024, 1, 1);

            WriteLine($"Поточна дата: {today}");
            WriteLine($"Через 5 днів: {today[5]}");
            WriteLine($"5 днів тому: {today[-5]}");

            WriteLine($"Чи є {today} останнім днем місяця? {!today}");

            if (JanFirst) WriteLine($"{JanFirst} - це початок року!");

            string dateStr = today; 
            WriteLine($"Рядок з дати: {dateStr}");

            Date parsedDate = (Date)"2025-05-20";
            WriteLine($"Дата з рядка: {parsedDate}");
            WriteLine();


            // --- Тестування Завдання 2: Клас VectorByte ---
            WriteLine("=== ТЕСТУВАННЯ КЛАСУ VectorByte ===");
            VectorByte v1 = new VectorByte(3, 10);
            VectorByte v2 = new VectorByte(3, 5);

            Write("Вектор V1: "); v1.Output();
            Write("Вектор V2: "); v2.Output();

            VectorByte vSum = v1 + v2;
            Write("V1 + V2: "); vSum.Output();

            VectorByte vScalar = v1 + (byte)50;
            Write("V1 + 50 (скаляр): "); vScalar.Output();

            v1++;
            Write("V1 після ++: "); v1.Output();

            byte val = v1[10];
            if (v1.CodeError == -1) Console.WriteLine("Помилка: Індекс вектора поза межами! (CodeError -1)");

            WriteLine($"Кількість створених векторів: {VectorByte.CountVectors()}");
            WriteLine();


            // --- Тестування Завдання 3: Клас MatrixByte ---
            WriteLine("=== ТЕСТУВАННЯ КЛАСУ MatrixByte ===");
            MatrixByte matrix = new MatrixByte(2, 3, 2);
            WriteLine("Матриця 2x3:");
            matrix.Output();

            matrix[4] = 99;
            WriteLine("Матриця після зміни matrix[4] = 99:");
            matrix.Output();

            VectorByte vecForMul = new VectorByte(3, 1);
            VectorByte resultVec = matrix * vecForMul;
            Write("Результат множення Matrix * Vector(1,1,1): ");
            resultVec.Output();

            WriteLine("\nТест завершено. Натисніть будь-яку клавішу...");
            ReadKey();

        }
    }
}
