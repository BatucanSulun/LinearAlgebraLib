using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Net.Security;
using System.Numerics;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

//

#region Karalama
//Matrix<float> denklem = new(3, 3);
//denklem.AddValue(1, 0);
//denklem.AddValue(2, 1);
//denklem.AddValue(-3, 2);
//denklem.AddValue(2, 3);
//denklem.AddValue(-1, 4);
//denklem.AddValue(4, 5);
//denklem.AddValue(1, 6);
//denklem.AddValue(-1, 7);
//denklem.AddValue(1, 8);

//Matrix<float> b = new(1, 3);
//b.AddValue(6, 0);
//b.AddValue(1, 1);
//b.AddValue(3, 2);

//Console.WriteLine("İşlem Yapmadan Önce");
//denklem.Print();
//Console.WriteLine("Üç Katı alınmış Hali");
//Matrix<float> ucKati = denklem.MapCopy(x => x * 3);
//ucKati.Print();

//Matrix<float> result = denklem.Solve(b);
//Matrix<float> matrisinTersi = denklem.DecomposeToLu();
//Console.WriteLine("Matrisin Tersi");
//matrisinTersi.Print();
//Console.WriteLine("Sonuç");
//result.Print();

//int[] values = new int[3];
//values[0] = 1;
//values[1] = 2;
//values[2] = 3;
//int sum = 0;
//for (int i = 0; i < values.Length; i++)
//{
//    for (int k = i + 1; k < values.Length; k++)
//    {
//        sum += values[k] * values[i];
//    }
//}
//Console.WriteLine($"Toplam: {sum}");
//Matrix<float> A = new(3, 3);
//Matrix<float> B = new(3, 3);
//A.AddValue(2, 0);
//A.AddValue(1, 1);
//A.AddValue(3, 2);
//A.AddValue(1, 3);
//A.AddValue(0, 4);
//A.AddValue(2, 5);
//A.AddValue(4, 6);
//A.AddValue(1, 7);
//A.AddValue(2, 8);
//Console.WriteLine("A Matrisi:");
//A.Print();
//B.AddValue(1, 0);
//B.AddValue(0, 1);
//B.AddValue(1, 2);
//B.AddValue(0, 3);
//B.AddValue(1, 4);
//B.AddValue(3, 5);
//B.AddValue(2, 6);
//B.AddValue(1, 7);
//B.AddValue(2, 8);
//Console.WriteLine("B Matrisi");
//B.Print();
//Matrix<float> result = A * B;
//Console.WriteLine("İki Matrisin Çarpımı");
//result.Print();
//Console.WriteLine("Çarpımın Transpozu");
//Matrix<float> transposeResult = result.Transpose();
//transposeResult.Print();
//Console.WriteLine("A Matrisinin Tersi:");
//Matrix<float> Ainvers = A.DecomposeToLu();
//Ainvers.Print();
//Console.WriteLine($"B'nin determinatı: {B.GetDeterminant()}");
//Console.WriteLine($"B'nin Tersi:");
//Matrix<float> bInverse = B.DecomposeToLu();
//bInverse.Print();
//Console.WriteLine($"A'nin determinatı: {A.GetDeterminant()}");
//Console.WriteLine($"A'nin Tersi:");
//Matrix<float> aInverse = B.DecomposeToLu();
//aInverse.Print();
//Matrix<float> A = new(1, 3);
//Matrix<float> B = new(1, 3);
////A.AddValue(2, 0);
////A.AddValue(3, 1);
////B.AddValue(-2,0);
////B.AddValue(-3, 1);
//A.AddValue(3, 0);
//A.AddValue(3, 1);
//A.AddValue(3, 2);
//B.AddValue(1, 0);
//B.AddValue(0, 1);
//B.AddValue(4, 2);

//Console.WriteLine($"A büyüklük {A.Magnitude()}");
//Console.WriteLine($"B büyüklük {B.Magnitude()}");
//Console.WriteLine($"A.B: {A.Dot(B)}");

//Console.WriteLine($"İki vektör arasındaki açı: {A.Angle(B)}");

//Matrix<float> C = new(1, 4);
//Matrix<float> D = new(1, 4);
//C.AddValue(0, 0);
//C.AddValue(-2, 1);
//C.AddValue(-1, 2);
//C.AddValue(1, 3);
//D.AddValue(-3, 0);
//D.AddValue(2, 1);
//D.AddValue(4, 2);
//D.AddValue(4, 3);

//Console.WriteLine($"C büyüklük {C.Magnitude()}");
//Console.WriteLine($"D büyüklük {D.Magnitude()}");
//Console.WriteLine($"A.B: {C.Dot(D)}");
//Console.WriteLine($"İki vektör arasındaki açı: {C.Angle(D)}");
//Matrix<float> u = new(1, 2);
//Matrix<float> v = new(1, 2);
//v.AddValue(2, 0);
//v.AddValue(4, 1);
//u.AddValue(1, 0);
//u.AddValue(1, 1);

//Matrix<float> proj =u.Project(v);   
//proj.Print();
//Console.WriteLine($"Projction vekötrün büyüklüğü: {proj.Magnitude()}"); 
//Console.WriteLine($"v'nin u üzerindeki vektörün büyüklüğü {v.ScalarProjection(u)}");//v'nin üzerine yansıya u vektörünün büyüklüğ
//Scalar proj ile magnitude değeri aynı olamaz çünkü sclara proj içerisinde yön de var mangitude sadece büyüklük
//Burada yuvarlama hataları var bu hataları nasıl halledeceğiz?

//string metin = "abc";// Hafızada yeri caer/// strings are immutable 
//metin += "d";// hafızada yeni bir yer 
#endregion

Matrix<float> point = new(1, 3);
point.AddValue(1, 0);
point.AddValue(2, 1);
point.AddValue(2, 2);

Matrix<float> planeOrigin = new(1, 3);
planeOrigin.AddValue(1, 0);
planeOrigin.AddValue(0, 1);
planeOrigin.AddValue(0, 2);

Matrix<float> planeNormal = new(1, 3);
planeNormal.AddValue(2, 0);
planeNormal.AddValue(-1, 1);
planeNormal.AddValue(2, 2);
    
Matrix<float>enYakinKoordinat= point.ProjectOnPlane(planeOrigin, planeNormal);

Console.WriteLine("En Yakın Nokta:");
enYakinKoordinat.Print();

class Matrix<T> where T : IFloatingPointIeee754<T> //Generic math sınıfını kullanmak için IFloat tipinden kalıtım aldık.
{
    // Epsilon'u T tipine göre dinamik üretiyoruz.
    private static readonly T epsilon = T.CreateChecked(1e-9); //Bunu static yapıp cache'lemek iyi bir seçim mi emin değilim?
    readonly int row;
    readonly int col;
    private T[] _values; // Artık double değil, T tipinde.
    int TotalElements => row * col; //Bu pek de lazım değil aslında.
    private static Random _random = new(); //Random nesnesini metodun içerisinde oluşturmak çok sağlıklı bir yaklaşım değil neden ?? For döngüsü içinde çağrıldığı zaman bilgisayar clock'u aynı kalacağı için bütün random vektörleri aynı değeri alırdı.Buna biraz daha detaylı bak.

    public Matrix(int row, int col)
    {
        this.row = row;
        this.col = col;
        this._values = new T[TotalElements];
    }

    public int GetRow() => row;
    public int GetColumn() => col;
    public int GetLength() => row * col;
    public T ReadValue(int row, int col) => this[row, col];

    public void AddValue(T value, int index) => _values[index] = value;
    public bool IsSquareMatrix() => row == col;
    public void Print()
    {
        StringBuilder sb = new(); //
        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                sb.Append(_values[r * col + c] + " ");
            }
            sb.AppendLine();
        }
        sb.ToString(); // Doğru ve en performanslı kullanım!

        Console.WriteLine(sb);
        //Console.WriteLine(sb.ToString()); //Böyle olsa ne fark eder ??
    }

    /// <summary>
    /// Matrisi bir boyuttan iki boyuta çevirmemizi sağlayan indexer. Nesnenin kendisini bir array gibi kullanmamızı sağlıyor.
    /// </summary>
    /// <param name="row">Matrisin satırını belirtir.</param>
    /// <param name="col">Matrisin sütununu belirtir.</param>
    /// <returns></returns>
    private T this[int row, int col]
    {
        get
        {
            return _values[row * this.col + col];
        }
        set
        {
            _values[row * this.col + col] = value;
        }
    }

    /// <summary>
    /// Bir Matrisin Satırlarını Sütun, Sütunlarını ise Satır Yapar
    /// </summary>
    /// <returns></returns>
    public Matrix<T> Transpose()
    {
        Matrix<T> result = new(col, row);
        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < col; c++)
            {
                result[c, r] = this[r, c];
            }
        }
        return result;
    }

    /// <summary>
    /// Bütün elemanları sıfır olan bir matris getirir.
    /// </summary>
    public void ZeroMatrix()
    {
        Array.Fill(_values, T.Zero);
    }

    /// <summary>
    /// Matrisin birim matris olup oladığının kontrolünü yapar.
    /// </summary>
    /// <returns></returns>
    public bool IsIdentityMatrix()
    {
        if (row != col) return false; //Kare matrix kontrolü yapılıyor.

        for (int r = 0; r < row; r++)
        {
            // T.One generic olarak 1'i temsil eder. Math.Abs yerine T.Abs kullanıyoruz.
            if (T.Abs(this[r, r] - T.One) > epsilon)
            {
                return false;
            }
            for (int c = r + 1; c < col; c++)
            {
                if (T.Abs(this[r, c]) > epsilon || T.Abs(this[c, r]) > epsilon)
                {
                    return false;
                }
            }

        }

        return true;
    }

    /// <summary>
    /// Matrisin simetrik olup olmadığını kontrol eder. Diagonal boyunca elemanların simetrik olup olmadığını kontrol eder.
    /// </summary>
    /// <returns></returns>
    public bool IsSymmetricMatrix()
    {
        if (row != col) return false;

        for (int r = 0; r < row; r++)
        {
            for (int c = r + 1; c < col; c++)
            {
                if (T.Abs(this[r, c] - this[c, r]) > epsilon)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsDiagonalMatrix()
    {
        if (row != col) return false;

        for (int r = 0; r < row; r++)
        {
            for (int c = r + 1; c < col; c++)
            {
                if (T.Abs(this[r, c]) > epsilon || T.Abs(this[c, r]) > epsilon)
                {
                    return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// Matrisin Diagonalinin altındaki elemanların sıfır olup olmadığını kontrol eder. Diagonal üzerindeki elemanlar sıfır olabilir.
    /// </summary>
    /// <returns></returns>
    public bool IsLowerTriangularMatrix()
    {
        if (row != col) return false;

        // Alt üçgensel olması için köşegenin üstü (c > r) sıfır olmalı
        for (int r = 0; r < row; r++)
        {
            for (int c = r + 1; c < col; c++)
            {
                if (T.Abs(this[r, c]) > epsilon) // Üst taraf sıfır değilse false
                {
                    return false;
                }
            }
        }
        return true;
    }
    /// <summary>
    /// Matrisin Diagonalinin üstündeki elemanların sıfır olup olmadığını kontrol eder. Diagonal üzerindeki elemanlar sıfır olabilir.
    /// </summary>
    /// <returns></returns>
    public bool IsUpperTriangularMatrix()
    {
        if (row != col) return false;

        // Üst üçgensel olması için köşegenin altı (r > c) sıfır olmalı
        for (int r = 0; r < row; r++)
        {
            for (int c = 0; c < r; c++) // c < r olacak şekilde yani alt üçgeni tarıyoruz
            {
                if (T.Abs(this[r, c]) > epsilon) // Alt taraf sıfır değilse false
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    ///  Matrisin diagonali üzerindeki elemanların toplamını verir.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Matris eğer kare değilse hata fırlatır.</exception>
    public T TraceOfMatrix()
    {
        if (!IsSquareMatrix())
        {
            throw new InvalidOperationException("Matris izi sadece kare matrisler için hesaplanabilir.");
        }

        T result = T.Zero;
        for (int i = 0; i < row; i++) //Satır ve sütunu iki kere dönmene gerek yok tek döngüde işimiz halledebiliriz.
        {
            result += this[i, i];
        }

        return result;
    }
    public Matrix<T> DecomposeToLu()
    {
        // 1. ÖNCE KONTROL: Kare matris değilse tersi alınamaz.
        if (row != col)
        {
            throw new InvalidOperationException($"Boyut uyuşmazlığı: Matris Kare Değil. Matris Boyutları:({row}x{col})");
        }


        Matrix<T> inverse = GenerateIdentity(row);
        Matrix<T> upperTriangular = this.Copy();

        // 2x2 matris ise Adjoint yöntemi ile direkt metodu bitirelim.
        if (row == 2 && col == 2)
        {
            //Yeni ekleme: GetDeterminant() metodundan kurtulamak istediğim için burada 2x2 bir matris için determinan hesaplayacağım det=0 ise det=0 olduğu için tersi alınamaz diye hata fırlatacağım. 
            T det = (_values[0] * _values[3]) - (_values[2] * _values[1]);
            if (det == T.Zero)
            {
                throw new InvalidOperationException("Matrisin Determinantı Sıfır Olduğu İçin Tersi Yoktur.");

            }

            inverse[0, 0] = upperTriangular[1, 1] / det;
            inverse[0, 1] = -(upperTriangular[0, 1]) / det;
            inverse[1, 0] = -(upperTriangular[1, 0]) / det;
            inverse[1, 1] = upperTriangular[0, 0] / det;

            return inverse;
        }

        // 2. ADIM: Lower Triangular, Upper Triangular ve Permütasyon matrislerini tanımlayalım.
        Matrix<T> permutation = GenerateIdentity(row);
        Matrix<T> lowerTriangular = GenerateIdentity(row);

        // --- 3x3 ve DAHA BÜYÜK MATRİSLER ---

        // 1. Kısım: LU Ayrışımını (Decomposition) Yapma
        for (int p = 0; p < row; p++)
        {
            int maxRowIndex = p;
            T maxValue = T.Abs(upperTriangular[p, p]);
            for (int i = p + 1; i < row; i++)
            {
                T currentValue = T.Abs(upperTriangular[i, p]);
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxRowIndex = i;
                }
            }

            //Yeni eklediğim kod.En büyük değeri buluyor en büyük değer mutlak değere göre sıfırsa demek o kolonun hepsi sıfır demektir det=0 olur ve tersi yoktur.
            if (maxValue == T.Zero)
            {
                throw new InvalidOperationException("Matrisin Determinantı Sıfır Olduğu İçin Tersi Yoktur.");

            }

            if (p != maxRowIndex)
            {
                upperTriangular.SwapRows(p, maxRowIndex);
                permutation.SwapRows(p, maxRowIndex);

                // DÜZELTME 1: lowerTriangular matrisinde sadece o ana kadar doldurulan geçmiş sütunlar takas edilir!
                // Eğer tüm satırı SwapRows ile değiştirirsek, ilerideki sıfırlar ve köşegendeki 1'ler bozulur.
                for (int k = 0; k < p; k++)
                {
                    T temp = lowerTriangular[p, k];
                    lowerTriangular[p, k] = lowerTriangular[maxRowIndex, k];
                    lowerTriangular[maxRowIndex, k] = temp;
                }
            }

            // Pivot altını sıfırlama
            for (int r = p + 1; r < row; r++)
            {
                T scalar = -(upperTriangular[r, p] / upperTriangular[p, p]);
                upperTriangular.AddMultipleRow(p, r, scalar);

                // DÜZELTME 2: AddMultipleRow metodunda 'scalar' kullanıyorsak, yapılan işlemin TERSİ L matrisine yazılır.
                // Çıkarma işlemi yaptıysak L'ye eksi işaretlisi (yani pozitif halini) eklemeliyiz.
                lowerTriangular[r, p] = -scalar;
            }
        }

        // 3. ADIM: Forward ve Backward Substitution ile Ters Matrisi Bulma
        // L * U * x = P * e denklemini çözeceğiz. (e: birim matrisin sütunu)

        // Ters matrisin her bir sütununu tek tek bulmak için döngüye giriyoruz.
        for (int c = 0; c < col; c++)
        {
            // P * e aslında Permütasyon matrisinin c. sütunundan başka bir şey değildir!
            T[] b = new T[row];
            for (int i = 0; i < row; i++)
            {
                b[i] = permutation[i, c];
            }

            // A) Forward Substitution: L * y = b denkleminden 'y' vektörünü bul
            // L alt üçgensel olduğu için yukarıdan aşağıya (i = 0'dan başlar) çözülür.
            T[] y = new T[row];
            for (int i = 0; i < row; i++)
            {
                T sum = T.Zero;
                for (int k = 0; k < i; k++) //Döngünün k<i'ye kadar dönmesi köşegene gelene kadarki elemanları çarpmamızı sağlıyor.
                {
                    sum += lowerTriangular[i, k] * y[k]; //Köşegene gelene kadarki her elemanın toplamını veriyor.
                }
                // L'nin köşegeni her zaman 1 olduğu için bölme işlemi yapmamıza gerek yok.
                y[i] = b[i] - sum; //Köşegene elemanlarının çarpılıp çıkarlıması bize y'nin kendisini veriyor.
            }

            // B) Backward Substitution: U * x = y denkleminden 'x' vektörünü bul
            // U üst üçgensel olduğu için aşağıdan yukarıya (i = row - 1'den başlar) çözülür.
            T[] x = new T[row];
            for (int i = row - 1; i >= 0; i--)
            {
                T sum = T.Zero;
                for (int k = i + 1; k < row; k++)
                {
                    sum += upperTriangular[i, k] * x[k];
                }
                // U'nun köşegeni 1 olmak zorunda olmadığı için köşegen elemanına bölüyoruz.
                x[i] = (y[i] - sum) / upperTriangular[i, i];
            }

            // C) Bulduğumuz x vektörü, Ters Matrisin c. sütunudur! Matrise yerleştirelim.
            for (int i = 0; i < row; i++)
            {
                inverse[i, c] = x[i];
            }
        }

        return inverse;
    }

    /// <summary>
    /// Gauss-Jordan Yöntemi ile matrisin tersini alır.DİKKAT!: Pivot noktalarının üstünde kalan değerleri satır değiştirme işlemi yapamadığımız için  yuvarlama hatası(round-off error) yapabilir!! DİKKAT: Sıfır kontrolünü tolerans ile yapmıyoruz direkt olarak tipin T.Zero ile kontrolü yapılıyor.
    /// </summary>
    /// <exception cref="ArgumentException">Matris kare değilse hata fırlatır.</exception>
    public Matrix<T> GaussJordanInverse()
    {
        if (row != col)
        {
            throw new InvalidOperationException($"Boyut uyuşmazlığı: Matris Kare Değil. Matris Boyutları:({row}x{col})");
        }

        T det = GetDeterminant();
        if (det == T.Zero) //Epsilon ile kontrol yapılabilir.
        {
            throw new InvalidOperationException("Matrisin Determinantı Sıfır Olduğu İçin Tersi Yoktur.");
        }

        Matrix<T> copy = this.Copy();
        Matrix<T> inverse = GenerateIdentity(row);

        // 2x2 İÇİN OPTİMİZASYON (Kofaktör/Adjoint Yöntemi)
        if (row == 2 && col == 2)
        {

            inverse[0, 0] = copy[1, 1] / det;
            inverse[0, 1] = -(copy[0, 1]) / det;
            inverse[1, 0] = -(copy[1, 0]) / det;
            inverse[1, 1] = copy[0, 0] / det;

            return inverse;
        }

        // --- 3x3 ve ÜZERİ İÇİN GAUSS-JORDAN YÖNTEMİ ---

        // 1. AŞAMA: Aşağı Doğru Temizlik (Alt Üçgeni Sıfırlama)
        for (int p = 0; p < row; p++)
        {
            int maxRowIndex = p;
            T maxValue = T.Abs(copy[p, p]);

            for (int i = p + 1; i < row; i++)
            {
                T currentValue = T.Abs(copy[i, p]);
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxRowIndex = i;
                }
            }

            if (p != maxRowIndex)
            {
                copy.SwapRows(p, maxRowIndex);
                inverse.SwapRows(p, maxRowIndex); // İkiz kardeşe de aynı muamele! 
            }

            for (int r = p + 1; r < row; r++)
            {
                T scalar = -(copy[r, p] / copy[p, p]);

                copy.AddMultipleRow(p, r, scalar);
                inverse.AddMultipleRow(p, r, scalar);
            }
        }

        // 2. AŞAMA: Yukarı Doğru Temizlik (Üst Üçgeni Sıfırlama
        for (int p = row - 1; p >= 0; p--) // Sağ alt köşeden (son pivottan) başlıyoruz
        {
            for (int r = p - 1; r >= 0; r--) // Pivotun bir üstünden en üst satıra (0) kadar çıkıyoruz
            {
                T scalar = -(copy[r, p] / copy[p, p]);

                copy.AddMultipleRow(p, r, scalar); // Kaynak: p, Hedef: r.Karıştırma!!
                inverse.AddMultipleRow(p, r, scalar);
            }
        }

        // 3. AŞAMA: Köşegenleri 1 Yapma
        for (int p = 0; p < row; p++)
        {
            T scalar = copy[p, p];

            // Sadece inverse matrisini bölmek yeterlidir.Son adım olduğu için copy matrisin tamamen birim matrise dönmesinin bir anlamı yok.
            // Copy matrisiyle işimiz bitti, onu çöpe atacağız.
            for (int c = 0; c < col; c++)
            {
                inverse[p, c] /= scalar;
            }
        }

        return inverse;
    }

    /// <summary>
    /// Verilen matrisin determinantını alır.
    /// </summary>
    /// <returns>Matrisin T tipinden determinantını döndürür.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public T GetDeterminant()
    {
        //Önce kare matris mi kontrolü yapıyorum.Kare matris değilse hata fırlatıyorum.
        if (!IsSquareMatrix())
        {
            throw new InvalidOperationException($"Boyut uyuşmazlığı: Matris Kare Değil. Matris Boyutları:({row}x{col})");
        }
        return row switch //Daha modern bil syntax kullandık.
        {
            1 => _values[0],
            2 => (_values[0] * _values[3]) - (_values[2] * _values[1]),
            3 => (_values[0] * _values[4] * _values[8]) +
                    (_values[1] * _values[5] * _values[6]) +
                    (_values[2] * _values[3] * _values[7]) -
                    (_values[0] * _values[5] * _values[7]) -
                    (_values[1] * _values[3] * _values[8]) -
                    (_values[2] * _values[4] * _values[6]),
            _ => ComputeLargerMatrix() //Dewfault durumda veri tipi tuttuğu sürece metot çağırabiliyorsun.

        };
    }

    /// <summary>
    /// Matrisin determinantını LU Decomposiion yaparak alır.Matrisin Upper Triangular forma getirir ve diagonal üzerindeki elemanları çarpar. Ancak GetDeterminant Metodu tarafından sadece 3x3'ten büyük matrisler için çağrılır.
    /// </summary>
    /// <returns></returns>
    private T ComputeLargerMatrix()
    {
        // 1. ÖNCE KONTROL: Kendi orijinal halimiz zaten üst üçgensel mi?
        if (IsUpperTriangularMatrix())
        {
            return MultiplyDiagonal(0);
        }

        // 2. KOPYA OLUŞTURMA: İşlemler orijinali bozmasın diye kopya alıyoruz.
        Matrix<T> copy = this.Copy();
        int swapCount = 0;

        // 3. SÜTUNLARDA GEZME (Pivot = p)
        for (int p = 0; p < row; p++)
        {
            int maxRowIndex = p;
            T maxValue = T.Abs(copy[p, p]);

            // 3.1. Sütunda aşağıya doğru inerek (i. satır, p. sütun) en büyük mutlak değeri bul.
            for (int i = p + 1; i < row; i++)
            {
                T currentValue = T.Abs(copy[i, p]);
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxRowIndex = i;
                }
            }

            // 3.2. Eğer o sütunun altı tamamen sıfırsa determinant 0'dır, direkt çık.
            if (maxValue == T.Zero)
            {
                return T.Zero;
            }

            // 3.3. En büyük sayı başka satırdaysa sıfır tabanlı kendi iç metodunla takas yap.
            if (p != maxRowIndex)
            {
                // Senin yeni standardına göre makine dairesi metodu: SwapRows
                copy.SwapRows(p, maxRowIndex);
                swapCount++;
            }

            // 3.4. Pivotun altındaki satırları sıfırla.
            for (int r = p + 1; r < row; r++)
            {
                T scalar = -(copy[r, p] / copy[p, p]);

                // Senin yeni standardına göre makine dairesi metodu: AddMultipleRow
                copy.AddMultipleRow(p, r, scalar);
            }
        }

        // 4. Kopyanın köşegenlerini yardımcı metotla çarp ve sonucu dön.
        return copy.MultiplyDiagonal(swapCount);
    }

    /// <summary>
    /// Private erişim belirleyicili DecomposeToLU metodu ile aynı işi yapar.Ancak sadece determinantı değil matrisin Upper triangular formunu da döndürür.Bu metodu sadece Upper triangular form halini konsole'a bastırabilmek için yazdım.
    /// </summary>
    /// <returns>Hem matrisin determinantını hem de Matrisin Upper Triangular formunu döndürür.</returns>
    public (T Determinant, Matrix<T>) PublicDecomposeToLU() //Out keyword'ü yerine birden fazla parametre bir tuple ile iki değer döndürüyoruz
    {
        Matrix<T> copy = this.Copy();
        // 1. ÖNCE KONTROL: Kendi orijinal halimiz zaten üst üçgensel mi?
        if (IsUpperTriangularMatrix())
        {

            return (MultiplyDiagonal(0), copy);
        }

        // 2. KOPYA OLUŞTURMA: İşlemler orijinali bozmasın diye kopya alıyoruz.
        int swapCount = 0;

        // 3. SÜTUNLARDA GEZME (Pivot = p)
        for (int p = 0; p < row; p++)
        {
            int maxRowIndex = p;
            T maxValue = T.Abs(copy[p, p]);

            // 3.1. Sütunda aşağıya doğru inerek (i. satır, p. sütun) en büyük mutlak değeri bul.
            for (int i = p + 1; i < row; i++)
            {
                T currentValue = T.Abs(copy[i, p]);
                if (currentValue > maxValue)
                {
                    maxValue = currentValue;
                    maxRowIndex = i;
                }
            }

            // 3.2. Eğer o sütunun altı tamamen sıfırsa determinant 0'dır, direkt çık.
            if (maxValue == T.Zero)
            {

                return (T.Zero, copy);
            }

            // 3.3. En büyük sayı başka satırdaysa sıfır tabanlı kendi iç metodunla takas yap.
            if (p != maxRowIndex)
            {
                // Senin yeni standardına göre makine dairesi metodu: SwapRows
                copy.SwapRows(p, maxRowIndex);
                swapCount++;
            }

            // 3.4. Pivotun altındaki satırları sıfırla.
            for (int r = p + 1; r < row; r++)
            {
                T scalar = -(copy[r, p] / copy[p, p]);

                // Senin yeni standardına göre makine dairesi metodu: AddMultipleRow
                copy.AddMultipleRow(p, r, scalar);
            }
        }
        // 4. Kopyanın köşegenlerini yardımcı metotla çarp ve sonucu dön.
        return (copy.MultiplyDiagonal(swapCount), copy);
    }

    /// <summary>
    /// Matrisin satırlarını yer değiştirir.Temel satır işlemlerinden bir tanesi.
    /// </summary>
    /// <param name="row1">Row 2 buraya gelir</param>
    /// <param name="row2">Row 1 buraya gelir.</param>
    /// <exception cref="ArgumentOutOfRangeException">Matris boyutları dışında bir argüman verilirse hata fırlatır</exception>
    private void SwapRows(int row1, int row2) // public metot ile imzaları aynı olduğu için burada metot adını PrivateSwapRows olarak ekledim. Sınıf içerisinde diğer işlemlerde bu metodu kullanacağım.
    {
        if (row1 == row2) return;

        if (row1 < 0 || row1 >= this.row || row2 < 0 || row2 >= this.row)
        {
            throw new ArgumentOutOfRangeException(
                $"Geçersiz satır indeksi. Matris {this.row} satıra sahip. İstenen indeksler: {row1}, {row2}");
        }
        T temp;
        for (int c = 0; c < col; c++)
        {

            //Kütüphaneyi kullanan kişi indeksin sıfırdan başladığını bilmeyeceği için -1 çıkartma işlemini ekledim.Doğru mu yanlışş mı ?? Tahminim yanlış row1 ve row2 için T temp değişkeni tanımlarken yeni değişken tanımlayarak kodlasak daha iyi olur gibiydi.
            temp = this[row1, c];
            this[row1, c] = this[row2, c];
            this[row2, c] = temp;
        }
    }

    /// <summary>
    /// SwapRow metodunun sadece public hali.Konsole'da index'in 0'dan başladığını bilmeyen birinin bile kullanabileceği şekilde 0-based değil 1-based kodladım.Zaten metot gövdesinde private SwapRows metodı çağrısı vardır.
    /// </summary>
    /// <param name="row1">Row 2 bu satıra gelir</param>
    /// <param name="row2">Row 1 bu satıra gelir</param>
    /// <exception cref="ArgumentOutOfRangeException">1-based indesklemede matris boyutlarının dışında bir argüman verilirse bu hatayı fırlatır.</exception>
    public void PublicSwapRows(int row1, int row2)
    {
        if (row1 == row2) return;// Aynı satırı değiştirme herhangi bir işlem yapmaya gerek yok

        if (row1 <= 0 || row1 > this.row || row2 <= 0 || row2 > this.row) // sıfır veya sıfırdan küçük ve satır sayısından büyük argüman gelmesi durumunda hata fırlatıyoruz
        {
            throw new ArgumentOutOfRangeException(
             $"Geçersiz satır indeksi. Matris {this.row} satıra sahip. İstenen indeksler: {row1}, {row2}");
        }
        //Burada PrivateSwapRows metodunu çağırıp swap işlemini yapıyoruz.
        SwapRows(row1 - 1, row2 - 1);

    }

    /// <summary>
    /// Temel satır işlemlerinden bir diğeri.Bir satırın bir katını alarak başka bir satır ekler.
    /// </summary>
    /// <param name="sourceRow">Katı alınacak satırı belirtir.</param>
    /// <param name="targetRow">Katı alınan satırın hangi satıra ekleneceğini belirtir.</param>
    /// <param name="scalar">Katı alınan satırın kaç katının alınacağını belirtir.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void AddMultipleRow(int sourceRow, int targetRow, T scalar)
    {
        // 1. Skaler 0 ise eklenecek bir şey yoktur, döngüye girme
        if (EqualityComparer<T>.Default.Equals(scalar, default)) return;

        if (sourceRow < 0 || sourceRow >= this.row || targetRow < 0 || targetRow >= this.row)
        {
            throw new ArgumentOutOfRangeException(
                $"Geçersiz satır indeksi. Matris {this.row} satıra sahip. İstenen indeksler: {sourceRow}, {targetRow}");
        }

        for (int c = 0; c < this.col; c++)
        {
            this[targetRow, c] += this[sourceRow, c] * scalar; // Temel satır işlemleri bir satırın bir katı diğer bir satıra eklenmesi olarak tanımlandığı için eğer bir satırdan başka bir satırı çıkarmak istiyorsak scalar parametresini negatif olarak verilmesi gerekiyor şeklinde düşündüm ve += operatörünü kullandım.Doğru mu yanlış mı ??
        }
    }


    /// <summary>
    /// AddMultipleRow metodu ile aynı işlemi yapar.Tek farkı erişim belirleyicisidir.Kullanıcı 0-Based indexleme bilmeden 1-based indeksleme ile saıtr işlemleri yapabilir.Zaten metot gövdesinde private erişim belirleyicili AddMultipleRow metot çağrısı bulunur
    /// </summary>
    /// <param name="sourceRow"></param>
    /// <param name="targetRow"></param>
    /// <param name="scalar"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void PublicAddMultipleRow(int sourceRow, int targetRow, T scalar)
    {
        if (EqualityComparer<T>.Default.Equals(scalar, default)) return;

        if (sourceRow <= 0 || sourceRow > row || targetRow <= 0 || targetRow > row)
        {
            throw new ArgumentOutOfRangeException(
            $"Geçersiz satır indeksi. Matris {this.row} satıra sahip. İstenen indeksler: {sourceRow}, {targetRow}");
        }

        AddMultipleRow(sourceRow - 1, targetRow - 1, scalar);
    }

    /// <summary>
    /// Diagonal üzerindeki elemanları çarpar.Ancak UpperTriangular formata çevirirken satır değiştirme işlemi determinantın işaretini değiştirdiği için tek sayıda işaret değiştirildiyse determinantı eksi bir ile çarpar.
    /// </summary>
    /// <param name="swapCount">Kaç kere satır değiştirme işlemi yaptığını belirtir.</param>
    /// <returns></returns>
    private T MultiplyDiagonal(int swapCount)
    {
        T result = T.One;
        for (int i = 0; i < this.row; i++)
        {
            result *= this[i, i];
        }

        // Takas sayısı tek ise determinantın işaretini değiştir
        if (swapCount % 2 != 0)
        {
            result = -result;
        }

        return result;
    }

    /// <summary>
    /// Bir matrisin kopyasını çıkartan metot.
    /// </summary>
    /// <returns>T tipinden matris ve kopyasını aldığım matris ile aynı boyutlarda bir matris döndürür.</returns>
    public Matrix<T> Copy()
    {
        Matrix<T> copy = new(this.row, this.col);
        Array.Copy(this._values, copy._values, this._values.Length); //for döngüsü ile dönmeden daha maliyetsizdir!
        return copy;
    }

    public void MatrixPower(double power)
    {
        // Diagonal matris ise doğrudan formülle (T.Pow) ilerliyoruz.
        if (IsDiagonalMatrix())
        {
            for (int i = 0; i < row; i++)
            {
                // Veri uyuşmazlığı T.CreateChecked ve T.Pow kullanılarak çözüldü.
                this[i, i] = T.Pow(this[i, i], T.CreateChecked(power));
            }
        }
        else
        {
            // KONTROL 1: Diagonal olmayan bir matrisin ondalıklı kuvveti bu yöntemle alınamaz.
            if (power % 1 != 0)
            {
                throw new ArgumentException("Diagonal olmayan matrisler için sadece tam sayı kuvvetler hesaplanabilir.");
            }

            // KONTROL 2: Negatif kuvvet için matrisin tersini (Inverse) almak gerekir. 
            // Henüz Inverse metodumuz olmadığı için işlemi engelliyoruz.
            if (power < 0)
            {
                throw new NotSupportedException("Negatif kuvvet hesaplaması için matris tersi (Inverse) işlemi gereklidir.");
            }

            // KONTROL 3: Kuvvet 0 ise sonuç birim matris olmalıdır (Matematik kuralı: A^0 = I)
            if (power == 0)
            {
                Matrix<T> identity = Matrix<T>.GenerateIdentity(row);
                Array.Copy(identity._values, this._values, identity._values.Length);
                return;
            }

            // Artık power'ın pozitif bir tam sayı olduğundan eminiz. Döngü için int'e çevirebiliriz.
            int intPower = (int)power;

            Matrix<T> temp = Matrix<T>.GenerateIdentity(row);

            for (int i = 0; i < intPower; i++)
            {
                temp *= this;
            }

            // Geçici matristeki sonuçları ana matrisimize (this) kopyalıyoruz.
            Array.Copy(temp._values, this._values, temp._values.Length);
        }
    }


    /// <summary>
    /// /Matris içerisindeki her bir sayının teker teker kuvvetini alan fonksiyon.
    /// </summary>
    /// <param name="power">Elemanların kaçıncı kuvvetini alınacağını belirtir</param>
    public void ElementPower(double power)
    {
        if (power == 1) return; //return getirebilecek durumları ilk başa yazarak gereksiz yere if bloklarının çalışmasını önüne geçiyoruz.

        if (power == 0) //Bir sayının sıfırıncı kuvveti 1'dir. Bunun kontrolünü yapalım.Kuvvet sıfır ise bütün elemanların hepsi 1 olmalı.Bu kod bloğu bu işe yarıyor.
        {
            for (int i = 0; i < _values.Length; i++)
            {
                this._values[i] = T.One; //Bütün elemanları bir yapıyorum.
            }
            return;
        }
        T powerT = T.CreateChecked(power); //Her for döngüsünde kullanmamak için burada bir tane yaratıyoruz.
        for (int i = 0; i < _values.Length; i++)
        {
            _values[i] = T.Pow(_values[i], powerT);
        }
    }

    public void ElementSquare()
    {
        ElementPower(2);
    }
    public void ElementCube()
    {
        ElementPower(3);
    }

    //Karesi alma metodunu tanımladım ama metot içerisine yine senden AreIdentical metodunda  öğrendiğim başka bir metodu çağırma yöntemini kullandım.Bu doğru mu 

    public void MatrixSquare()
    {
        MatrixPower(2);
    }
    public void MatrixCube()
    {
        MatrixPower(3);
    }

    /// <summary>
    /// Bir vektörün büyüklüğünü, uzunluğunu hesaplayan metot
    /// </summary>
    /// <returns>Bir sayı döndürür</returns>
    /// <exception cref="InvalidOperationException"> Vektörün satırı veya sütunu bir değilse bu hatayı fırlatır.</exception>
    public T Magnitude() 
    {
        //Eğer gerçek bir vektör ise satır veya sütun 1 olmalı.Bu kontrol bu yüzden var.
        if (row != 1 && col != 1)
        {
            throw new InvalidOperationException("Magnitude/Norm hesabı sadece vektörler (1xN veya Nx1) için yapılabilir.");
        }
        T sum = T.Zero;
        for (int i = 0; i < _values.Length; i++)
        {
            sum += _values[i] * _values[i];
        }

        return T.Sqrt(sum);
    }

    /// <summary>
    /// Vektörü normalize eden metot. Yönünü aynen koruyarak büyüklüğünü(Magnitude) bir yapan metot
    /// </summary>
    /// <returns>Bir vektör döndürür</returns>
    /// <exception cref="InvalidOperationException"> Normalize edilecek metot sıfır vektörü ise bu hatayı verir çünkü büyüklük sıfır olduğu için sıfıra bölme durumu gelir.</exception>
    public Matrix<T> GetNormalized()
    {
        T magnitude = this.Magnitude();
        if (magnitude == T.Zero)
        {
            throw new InvalidOperationException("Sıfır vektörü normalize edilemez (0'a bölme hatası).");
        }

        Matrix<T> copy = this.Copy();
        for (int i = 0; i < _values.Length; i++)
        {
            copy._values[i] /= magnitude;
        }
        return copy;
    }

    /// <summary>
    /// İki vektörün iç çarpımını (dot product) verir
    /// </summary>
    /// <param name="v"> İç çarpımı alınacak vektörü temsil eden metot argümanı</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">Dot product yapılacak iki vektörün boyut kontrolünü yapar. Vektörlerin ya sütunları bir ya da satırları bir tane olmalıdır. Ayrıca iki vektörün boyutlarının da aynı olup olmadığını kontrol eder.</exception>
    public T Dot(Matrix<T> v)
    {
        //Önce boyut kontrolü yapalım.Boyutlar tutmuyorsa hata fırlatıp çıkarız.Bilgi kaybını engellemek için kopyalama işlemini yapmamıza da gerek yok

        if ((this.row != 1 && this.col != 1) || (v.row != 1) && (v.col != 1))
        {
            throw new InvalidOperationException($"Çarpılacak Matrislerin Boyutları uyuşmuyor." +
                $"Çarpılacak vektörlerin satır veya sütunlarından en az birinin kesinlikle 1 olması lazım" +
                $"Matris Boyutları:({this.row}x{this.col}) ve {v.row}x{v.col}) ");
        }
        if (this._values.Length != v._values.Length)
        {
            throw new InvalidOperationException($"Vektör uzunlukları uyuşmuyor: {this._values.Length} ve {v._values.Length}. Dot Product yapılamaz.");
        }

        T sum = T.Zero;
        for (int i = 0; i < this._values.Length; i++)
        {
            sum += this._values[i] * v._values[i];
        }
        return sum;
    }

    /// <summary>
    /// Bir vektörün argüman olarak verilen vektör ile arasındaki açıyı hesaplar.
    /// </summary>
    /// <param name="B">Nesne ile arasındaki açı hesaplanacak diğer vektör.</param>
    /// <returns>İki vektörün arasındaki açıyı derece cinsinden döner</returns>
    /// <exception cref="InvalidOperationException"> Her iki vektörden birinin büyüklüğü sıfır ise hata fırlatır. Çünkü sıfır vektörünün açısı tanımsızdır.</exception>
    public T Angle(Matrix<T> B)
    {
        T magnitudeA = this.Magnitude();
        T magnitudeB = B.Magnitude();
        //Vektörlerden birinin uzunluğu sıfır ise hata fırlatıyoruz
        if (magnitudeA == T.Zero || magnitudeB == T.Zero)
        {
            throw new InvalidOperationException("Sıfır vektörünün (uzunluğu 0 olan) açısı tanımsızdır.");
        }

        T dot = this.Dot(B);
        T angle = dot / (magnitudeA * magnitudeB);//Bu işlem sonucu radyan çıkıyor radyandan açıya gitmemiz lazım
        angle = T.Clamp(angle, -T.One, T.One);//Cauchy-Schwarz kuralı sayesinde u.v<= ||u||.||v|| olsuğunu biliyoruz.Metodu bir nevi "kurşun işlemez" hale getiriyoruz.
        return T.RadiansToDegrees(T.Acos(angle));//T.Acos(angle) bu işlem sonucu yine radyan çıkıyor bu çıkan hala radyan.Bu radyanı açıya döndürmemiz lazım.
    }

    /// <summary>
    /// Bir vektörün argüman olarak verilen vektörün üzerine yansıma vektörünü bulur.Dikkat et yansıma vektörünün büyüklüğünü bulmaz.Vektörün direkt kendisini verir.
    /// </summary>
    /// <param name="B">Üzerine dik izdüşüm alınacak vektör</param>
    /// <returns>Yansıma vektörünü döndürür.Dikkat izdüşüm vektörünün koordinatlarını veya büyüklüğünü vermez vektörün kendisini verir.</returns>
    /// <exception cref="InvalidOperationException"> Sıfır vektörü üzerine izdüşüm alınamaz çünkü payda sıfır olursa tanımsızlık durumuna girer.</exception>
    public Matrix<T> Project(Matrix<T> B)
    {
        #region Metodun eski hali.Bilerek silmedim saklıyorum

        ////DİKKAT: proj=(v.u)/(u.u).u formulünü kullanarak da bu işlem yapılabilir ancak bu yöntem daha performanslı olduğu için bunu tercih ettim.
        ////1.Adım:B matrisinin sıfır olması bir sorun oluştururken A matrisinin sıfır olmasında herhangi bir sorun yoktur.Hiçliğin, sıfır vektörünün başka bir vektörü üzerindeki gölgesi sıfırdır. Bu yüzden A sıfır kontrolü yapmıyoruz.
        //T magnitudeB = B.Magnitude();
        //if (magnitudeB == T.Zero)
        //{
        //    throw new InvalidOperationException("Sıfır vektörünün (uzunluğu 0 olan) açısı tanımsızdır.");
        //}
        ////2.Adım: B vektörünün normalini alarak izdüşüm vektörünün hangi yönde olduğunu bulalım
        //Matrix<T> proj = B.GetNormalized();
        ////3.Adım: Bu iki vektöün iç çarpımını bulalım.Bu değer bize projection'ın B yönünde ne kadar "uzayacağını" veya "ilerleyeceğini" verir
        //T scalar = this.Dot(proj);
        ////4.Adım: normB vektörünün her bir elemanını iç çarpım ile çarpıp "esnetelim"

        ////for (int i = 0; i < this._values.Length; i++)
        ////{
        ////    proj._values[i] *= scalar;
        ////}
        //return proj*scalar;
        #endregion
        //Temel Mantık. proj= ((n.v)/(n.n)).n formulünü kullanır n üzerine yansıma alınacak vektör v ise yansıtıalcak vekötür. 
        //Diğer bir tabirle= (this.B)/(B.B)*B işlemini yapar.
        //0.Adım: B vektörü yani üzerine vektör yansıtılacak vektörün sıfır olup olmadığını kontrol edelim. B sıfır ise sıfır vektörü üzerine yansıma yapılamaz.
        // ||B||^2= B.B demek. B sıfır ise zaten hem büyüklüğü hem de iç çarpımı sıfır olacaktır.
        T sqrMagB = B.Dot(B);

        if (sqrMagB == T.Zero)
        {
            throw new InvalidOperationException("Sıfır vektörü üzerine izdüşüm alınamaz.");
        }

        T dotProduct = this.Dot(B);
        T scalar = dotProduct / sqrMagB;
        return B * scalar;
    }
    /// <summary>
    /// Argüman olarak alınan vektörün üzerine dik izdüşümü alınan vektörün büyüklüğünü hesaplar.
    /// </summary>
    /// <param name="B">Üzerine izdüşüm alıancak vektörü temsil eden metot argümanı</param>
    /// <returns>İzdüşüm vektörünün büyüklüğünü temsil eden sayıyı ger döndürür</returns>
    public T ScalarProjection(Matrix<T> B)
    {

        //DİKKAT: Magnitude ASLA KULLANMAMALISIN çünkü magnitude hep pozitif değer getireceği için eğer iki vektör arasındaki açı 90'dan büyükse  proj arkada kalır ve yön olarak negatif kısmı kaybedersin.
        //DİKKAT: ||proj|| =(u.v)/||u|| formulü ile de yapılabilir ancak ufak da olsa performanstan kazandığımız için böyle yazdım.Hem dot product çağır hem magnitude çağır sonra böl nispeten daha uzun bir yol.
        //Bu yöntemle direkt olarak B yönündeki yansımanın büyüklüğünü buluyoruz
        return this.Dot(B.GetNormalized());
    }
    /// <summary>
    /// Bir vektörün üzerinde olmayan,tam aksine ona dik olan bileşenini döndüren metot.
    /// </summary>
    /// <param name="B">Üzerine dik izdüşümü alınacak metot </param>
    /// <returns>Dik bileşen vektörünü gönderir</returns>
    public Matrix<T> Reject(Matrix<T> B)
    {
        //Project metodu içerisinde zaten B ile ilgili bir kontrol yaptığım için burada tekrardan kontrol yapmama gerek yok.
        Matrix<T> proj = this.Project(B);
        return this - proj;
    }

    /// <summary>
    ///  Çapraz çarpım metodu. İki vektöre dik olan üçüncü bir dik vektörü döndüren metot.Ek olarak, metodun döndürdüğü vektörün büyüklüğü iki vektörü arasında oluşan paralelkenarın alanına eşittir
    /// </summary>
    /// <param name="B">Çapraz çarpıma sokulacak ikinci vektörü temsil eden metot argümanı</param>
    /// <returns>Çapraz çarpım sonucu çıkan dik vektörü döndürür</returns>
    /// <exception cref="InvalidOperationException">Çapraz çarpımı sadece 3D için yapar.Bu yüzden vektörler 3D değilse ve vektörlerin birbirlerine göre boyutları tutmuyorsa hata fırlatır </exception>
    public Matrix<T> Cross(Matrix<T> B)
    {
        //1.Adım: Önce boyut kontrolü yapmamız lazım.Boyut kontrolü:Cross product sadece R^3'te geçerlidir. Bu yüzden col!=3 kontrolü yaptım.Bu sayede hem 1x3 hem de 3x1 için çalışır.
        if (this._values.Length != 3 || B._values.Length != 3)
        {
            throw new InvalidOperationException($"Cross product sadece 3 boyutlu vektörlerde çalışır.");
        }

        if (this._values.Length != B._values.Length)
        {
            throw new InvalidOperationException($"Vektör uzunlukları uyuşmuyor: {this._values.Length} ve {B._values.Length}. Cross Product yapılamaz.");
        }
        //2.Adım: return edececeğimiz sonuç vektörünü tanımlayalım
        Matrix<T> result = new Matrix<T>(this.row, this.col);
        //3.Adım: Ctross-product işlemini yapalım. Hazır formulü kullanıp direkt olarak indeks ile atama yapacağım.
        result._values[0] = (this._values[1] * B._values[2]) - (this._values[2] * B._values[1]);
        result._values[1] = (B._values[0] * this._values[2]) - (this._values[0] * B._values[2]);
        result._values[2] = (this._values[0] * B._values[1]) - (this._values[1] * B._values[0]);
        return result;
    }
    /// <summary>
    /// 2D için çapraz çarpım yapan metot.Cisimlerin birbirlerine göre konumları hesaplamak için kullanılır.Verilen vektörler iki boyutlu olmalıdır.
    /// </summary>
    /// <param name="B">Çapraz çarpıma sokulacak ikinci metodu temsil eden metot argümanı </param>
    /// <returns>Çapraz çarpım sonucu çıkan metodu döndrür.</returns>
    /// <exception cref="InvalidOperationException">2D'de çapraz çarpım yaptığı için boyutların 2D olmaması durumunda hata fırlatır</exception>
    public T Cross2D(Matrix<T> B)
    {
        //1.Adım: Önce boyut kontrolü yapmamız lazım. 2x1 veya 1x2 değilse hata fırlatmalı        
        if (this._values.Length != 2 || B._values.Length != 2)
        {
            throw new InvalidOperationException($"Cross product sadece 2 boyutlu vektörlerde çalışır.");
        }
        //2.Adım: İlgili indekselerle çarparak T tipinden returnleyelim.
        return (this._values[0] * B._values[1]) - (this._values[1] * B._values[0]);
    }

    /// <summary>
    /// Çapraz çarpım sonucu çıkan vektörü normalize eder.
    /// </summary>
    /// <param name="B">Çapraz çarpıma sokulacak ikinci vektörü temsil eden metot argümanı</param>
    /// <returns>Büyüklüğü bir olan vektör döndürür</returns>
    public Matrix<T> CrossNormalized(Matrix<T> B)
    {
        //1.Adım: İki vekörün Cross product'ını hesaplayalım
        Matrix<T> crossAB = this.Cross(B);
        //2.Adım: GetNormalized() metodunu çağırıp returnleyelim
        return crossAB.GetNormalized();
    }


    /// <summary>
    /// Scalar Triple Product metodu.Hem argüman olarak hem de çağrıldığı nesnenin vektör olduğunu kabul eder.Herhangi bir şekilde noktadan vektör hesaplama yapmaz.B ve C vektörlerini çapraz çarpıma sokup çıkan metodu nesne ile iç çarpıma sokar. A.(BxC) formulüne göre çalışır.
    /// </summary>
    /// <param name="B">Çapraz çarpıma sokulacak ilk vektör</param>
    /// <param name="C">Çapraz çarpıma sokulacak ikinci vektör</param>
    /// <returns>Bir sayı döndürür.Sayı sıfır değilse vektörler coplanar/eşdüzlemli değildir.</returns>
    /// <exception cref="InvalidOperationException">Cross çarpım yaptığı için vektörlerin 3D'de olup olmadığının kontrolü sonucu hata fırlatır.</exception>
    public T ScalarTriple(Matrix<T> B, Matrix<T> C)
    {
        //1.Adım: Önce boyut kontrolü yapmamız lazım çünkü ScalarTriple sadece R^3'te sonuç verir.
        if (this._values.Length != 3 || B._values.Length != 3 || C._values.Length != 3)
        {
            throw new InvalidOperationException($"Scalar Triple Product sadece 3 boyutlu vektörlerde çalışır. " +
            $"Verilen boyutlar: A({this._values.Length}), B({B._values.Length}), C({C._values.Length})");
        }
        //2.Adım: B ve C vektörlerinin cross çarpımını bulmam lazım.Boyut kontrolü yapmıyorum çünkü çağırdığım metot Cross metodu zaten boyutlar tutmuyorsa hata fırlatıyor doğru mu yanlış mı ?
        Matrix<T> crossBC = B.Cross(C);
        //3.Adım: BxC ile iç dot product değerini bulmamız lazım.
        return this.Dot(crossBC);
        //DİKKAT:Neden T.Abs() ile return etmedik? Normalde bu işlem hacim bulur ve hacim matematiksel olarak negatif olamaz.Ancak cross2D'de olduğu gibi yön tayini yapabilmemiz için bize + veya - işareti lazım. Sonuç + gelirse A vektörü BC
        //vektörünün üzerinde - gelirse A vektörü BC'nin altındadır. Sağ el kuralından anlaşılabilir.
        //Not:Sonucun sıfır gelmesi üç vektörün coplanar yani aynı düzlemde olduğu anlamına gelir.        
    }

    /// <summary>
    /// Matrisin her bir elemanını verilen sayı ile çarpar. Map() metodunun sadece çarpma için daraltılmış hali.
    /// </summary>
    /// <param name="scaleFactor">Matris elemanlarının çarpılacağı sayıyı temsil eder.</param>
    public Matrix<T> ScaleMatrix(T scaleFactor)
    {
        //1.Adım: Herhangi bir boyut kontrolü yapmamıza gerek yok gibid duruyor.Bu yüzden veri kaybını önlemek için matrisin kopyasını alalım
        Matrix<T> copy = this.Copy();
        //2.Adım:Bir döngü yardımı ile her elemanı scaleFactor ile çarpalım
        for (int i = 0; i < this._values.Length; i++)
        {
            copy._values[i] *= scaleFactor;
        }
        //3.Adım:Skalalandırılmış matrisi döndürelim
        return copy;
    }

    /// <summary>
    /// Hadamard çarpımını gerçekleştiren metot
    /// </summary>
    /// <param name="B">Hadamard çarpımına sokulacak ikinci matrisi temsil eden metot argümanı</param>
    /// <returns>Çarpım sonucu çıkan matrisi döndürür</returns>
    /// <exception cref="InvalidOperationException">Boyut kontrolü sonucu ortaya çıkan uyuşmazlık için hata fırlatır.</exception>
    public Matrix<T> HadamardProduct(Matrix<T> B)
    {
        //1.Adım:Boyut kontrolü yapalım.
        if (this.row != B.row || this.col != B.col)
        {
            throw new InvalidOperationException($"Hadamard Çarpımı için Boyut tutmuyor. " +
            $"Verilen boyutlar: A({this.row}x{this.col}), B({B.row}x{B.col})");
        }
        //2.AdımSonuç matrisi tanımlayalım:Sonuç matrisi yine A ile veya B ile aynı boyutta olmalı
        Matrix<T> result = new Matrix<T>(this.row, this.col);
        //3.Adım:for döngüsü yardımı ile result matrisini dolduralım
        for (int i = 0; i < result._values.Length; i++)
        {
            result._values[i] = this._values[i] * B._values[i];
        }
        //4.Adım: result matrisini döndürelim
        return result;
    }
    //Hadamard çarpımı için ElementWiseProduct adında delegate ekleme işini yaptım.Dikkat et bu metot zaten nesne üzerinden çağrılacağı için herhangi bir şekilde this ile kullanmana gerek yok.

    /// <summary>
    /// Hadamard Çaprımını yapan ancak ismi farklı olan metot.Metot özünde HadamardPrduct() metodunun bir delegate'dir 
    /// </summary>
    /// <param name="B">Hadamard çarpımına sokulacak ikinci matrisi temsil eden metot argümanı</param>
    /// <returns>Hadamard çarpımı sonucu çıkan matrisi döndürür</returns>
    public Matrix<T> ElementWiseProduct(Matrix<T> B)
    {
        return HadamardProduct(B);
    }
    /// <summary>
    /// İki vektörün Dış Çarpımını bulan metot.
    /// </summary>
    /// <param name="B">Dış Çarpım yapılacak ikinci vektörü temsik eden metot argümanı</param>
    /// <returns>Çarpım sonucu oluşan matrisi döndürür</returns>
    /// <exception cref="InvalidOperationException">Boyut tutmaması durumnda fırlatırlan hata. İki vektörde ya sütun ya da bir boyutlu(satır vektörü)olmalı</exception>
    public Matrix<T> OuterProduct(Matrix<T> B)
    {
        //1.Adım: Boyut kontrolü.Nesnelerin satır veya sütun vektörü olup olmadığının kontrolünü yapalım
        bool isAVector = this.col == 1 || this.row == 1;
        bool isBVector = B.col == 1 || B.row == 1;
        if (!isAVector || !isBVector)
        {
            throw new InvalidOperationException($"Outer Product için Boyut tutmuyor. " +
       $"Verilen boyutlar: A({this.row}x{this.col}), B({B.row}x{B.col})");
        }

        //2.Adım: Sonuç vektörünün boyutlarını tanımalayalım
        int outRow = this._values.Length;
        int outCol = B._values.Length;
        //3.Adım: Sonuç vektörünü tanımlayalım
        Matrix<T> result = new Matrix<T>(outRow, outCol);
        //4.Adım: for döngüleri ile dönüp sonuç matrisine değer atayalım. 1D'den 2D'ye geçiş var.
        for (int i = 0; i < outRow; i++)
        {
            for (int j = 0; j < outCol; j++)
            {
                result[i, j] = B._values[j] * this._values[i];

            }
        }
        return result;
    }
    //Metodun çağrıldığı nesne vektör olarak değil başlangıç noktası olarak işlem yapıyor.
    //Dikkat!: Metot imzası sadece bir kesişim noktası ve bool gönderdiği için doğrunun düzlemin üzerinde olma durumunda sonsuz noktada kesişim gerçekleştiği için tek bir nokta seçmeden hayır kesişmiyor diyip false gönderir.Metodu kasıtlı olarak bu şekilde tasarladım.
    //Alternatif olarak beta==T.zero koşulunun içerisine alfaif (alfa == T.Zero intersectionPoint = this; kodu eklenerek doğrunun başlangıç noktası kesişim noktası olarak gönderilebilir.
    /// <summary>
    /// Doğru ile düzlemin kesişip kesişmediğini bulan metot.Nesnenin arrayindeki değerleri doğrunun başlangıç noktası olarak kabul eder.
    /// </summary>
    /// <param name="lineDir">Doğrunun doğrultu vektörü(Direction Vector)</param>
    /// <param name="planeOrigin">Düzlem üzerinde herhangi bir nokta</param>
    /// <param name="planeNormal">Düzlemin normali</param>
    /// <param name="intersectionPoint">Düzlem ile doğrunun kesişim noktası</param>
    /// <returns>Doğru ile düzlem kesişiyorsa true ve kesişim noktasını kesişmiyorsa false ve kesişm noktası olarak null döner</returns>
    public bool IntersectLineWithPlane(Matrix<T> lineDir, Matrix<T> planeOrigin, Matrix<T> planeNormal, out Matrix<T> intersectionPoint)
    {
        #region Varsayım 1
        //Doğru ile düzlemin kesişip kesişmediğini anlamak için parametrik doğru denklemini düzlem denkleminde yerine yazıp doğru denklemi yazarken kullandığımız t değişkeninin değerine göre yorum yapmamız lazım
        //Ancak, parametrik doğru denklemini x= a +dt şeklinde tanımlarsak. t diye bir değişkeni burada matematiksel anlamda tanımlayamayacağımız için bu kavramın "etrafından dolaşmamız lazım"
        #endregion
        #region Varsayım 2
        //Bu metodu çağırdığımız nesneni _values[] adlı array'inin içerisinde doğrunun yani line'ın başlangıç noktaları bulunuyor diye varsayım yaptım. Yanılmıyorsam sen de metot imzasında bu yüzden argüman olarak line'ın origin noktasını istemedin.Doğru mu yanlış mı?
        #endregion
        #region Varsayım 3
        //lineOrigin (a,b,c)
        //lineDir (d,e,f)
        //plnaeNormal(k,l,m)
        //planeOrigin(u,v,n)
        //şeklinde tanımlarsak 
        //beta= k*d+l*e+m*f //Biraz dikkatli bakarsan bu adım aslında planeNorm ile lineDir'in iç çarpımı olduğunu görürsün. Doğrunun yönü ile düzlem normalinin iç çarpımı sıfır ise birbirlerine diktir demektir.betanın sıfır olma durumunda zaten kesişmezler. Alttaki if kontrolü de bu işlemi yapıyor.
        //alfa=k*a+l*b+m*c-k*u-l*v-m*n olur.Bununda geometrik bir yorumu var ama tam olarak anlamadım.
        //Aslında çözmemiz gereken denklem t*beta+alfa=0 haline geliyor
        //beta sıfır ise doğru düzleme paraleldir false döndür
        //beta sıfır değil ancak alfa sıfır ise doğru ile düzlem doğrunun başlangıç noktasında kesişir
        //hem beta hem de alfa sıfır değil doğruyu t kadar scale edince/ölçeklendirince kesişim noktasını buluyoruz true gönderip out ile kesişim noktalarını gönderebiliriz. 
        //hem alfa hem beta'nın sıfır ise doğru düzlemin üzerinde yatmaktadır.Ancak beta sıfır olduğu için false gönderiyor.
        #endregion

        //Yukarıda yaptığım 3 varsayımı kullanarak bu metodu kodlayalım
        //1.Adım: betayı hesaplayalım
        intersectionPoint = null;
        //Burada doğrunun lineDir ile planeNorm arasında iç çarpım yapıyoruz
        T beta = Matrix<T>.Dot(lineDir, planeNormal);

        if (beta == T.Zero) // beta sıfır ise doğru ve düzlem birbirien paraleldir, kesişmez false döndürür.
        {
            return false;
        }
        //2.Adım:alfayı hesaplayalım.Alfanın sıfır çıkması demek doğru ile düzlem doğrunun başlangıç noktasında kesişiyor demektir.Burada yine aşternatif bir yol daha var ama kafam tam olarak kesmedi Her ne kadar tutarsız olsa da elle hesaplama yapmaı tercih ettim.
        T alfa = planeNormal._values[0] * this._values[0] +
                planeNormal._values[1] * this._values[1] +
                planeNormal._values[2] * this._values[2] -
                planeNormal._values[0] * planeOrigin._values[0] -
                planeNormal._values[1] * planeOrigin._values[1] -
                planeNormal._values[2] * planeOrigin._values[2];
        T t = -alfa / beta; //betanın sıfır olma durumunu yukarıdaki if bloğunda incelediğimiz için paydanın sıfır olma durumu için endişelenmemize gerek yok.

        intersectionPoint = new Matrix<T>(3, 1); // Varsayımsal 3x1 matris üretimi
        //Kesişim noktalarını hesaplayalım
        //Dikkat et alfanın sıfır olması durumunda t değişkeni de sıfır olacağı için doğru ile düzlem doğrunun başlangıç noktasında kesişecekler.
        //Aşağıdaki kodda alfa sıfır ise t'yi sıfır yapıp doğrunun başlangıç noktalarını döndürüyor.
        T x = this._values[0] + lineDir._values[0] * t;
        T y = this._values[1] + lineDir._values[1] * t;
        T z = this._values[2] + lineDir._values[2] * t;
        intersectionPoint._values[0] = x;
        intersectionPoint._values[1] = y;
        intersectionPoint._values[2] = z;
        return true;
    }

    // İki parametrik doğrunun kesişim noktasını bulur (Kesişim varsa true döner ve out parametresini doldurur)

    /// <summary>
    /// Parametrik formda olan iki doğrunun kesişip kesişmediğini döndüren metot.Nesnenin arrayindeki değerleri doğrunun başlangıç noktası olarak kabul eder.
    /// </summary>
    /// <param name="line1Dir">İlk doğrunun doğrultu vektörü(Direction vector)</param>
    /// <param name="line2Origin">kinci doğrunun başlangıç noktası</param>
    /// <param name="line2Dir">İkinci doğrunun doğrultu vektörü(Direction vector)</param>
    /// <param name="intersectionPoint">İki doğrunun kesişim noktası</param>
    /// <returns>Eğer iki doğru kesişiyorsa true ve kesişim noktalarını döner kesişmiyorsa false ve kesişim noktası olarak null döner</returns>
    public bool IntersectLineWithLine(Matrix<T> line1Dir, Matrix<T> line2Origin, Matrix<T> line2Dir, out Matrix<T> intersectionPoint)
    {
        //0.Adım: out parametresini tanımlayalım
        intersectionPoint = null;

        //1.Adım:Doğruların doğrultularının vektör çarpımını bulalım
        Matrix<T> crossDir = Matrix<T>.Cross(line1Dir, line2Dir); //Dikkat: Diyelim ki sıfır vektörü geldi. Uzunluğunun karesi de sıfır olacak. Uzunluğunun karesi demek aynı zamanda kendisi ile iç çarpıma sokmak demek.
        //Bu sayede sıfır vektörünün gelip gelmediğini kontrol ediyoruz.Yani uzunluğu bulurken karekök almak gibi CPU yorucu işlem yerine kendisi ile iç çaprıma bakıp yapıyoruz.Bu sayede zaten sıfır ise iç çarpım sonucu da sıfır gelecektir.||u||^2==u.u
        T w1 = Matrix<T>.Dot(crossDir, crossDir); //Çapraz çarpım vektörünün uzunluğunun karesi.
        //2.Adım: w1 sıfır ise doğrular paraleldir
        if (w1 == T.Zero)
        {
            return false;
        }
        //3.Adım: Ayrık doğru durumunu kodlayalım
        //3.1: Bir doğrunda diğer doğruya çizilen vektörü bulalım
        Matrix<T> deltaOrigin = line2Origin - this;
        //3.2: Bu vektör ile cross product çarpımı sonucu sıfırdan farklı bir değerse aykırı doğrular/skew lines idir.
        T scalarTripleProduct = Matrix<T>.Dot(deltaOrigin, crossDir);
        if (scalarTripleProduct != T.Zero)
        {
            return false;
        }

        //4.Adım:Aykırı ve paralel olma durumlarına baktıkArtık kesişimlerini kodlayabiliriz.
        Matrix<T> crossDeltaDir2 = Matrix<T>.Cross(deltaOrigin, line2Dir);
        T t = Matrix<T>.Dot(crossDeltaDir2, crossDir) / w1;

        //5.Adım: t'ye göre kesişim noktasını geri döndürelim
        intersectionPoint = new Matrix<T>(1, 3);
        intersectionPoint._values[0] = this._values[0] + t * line1Dir._values[0];
        intersectionPoint._values[1] = this._values[1] + t * line1Dir._values[1]; ;
        intersectionPoint._values[2] = this._values[2] + t * line1Dir._values[2]; ;
        return true;
    }

    /// <summary>
    /// Bir noktanın bir düzleme en kısa (dik) uzaklığını hesaplar.Nesnenin arrayinde tuttuğu noktaları noktanın koordinatları olarak alır.Sadece 3 boyutta işlem yapar.
    /// </summary>
    /// <param name="planeOrigin">Düzlemin orijini.Orijin bilinmiyorsa düzlem denklemini sağlayan bir nokta seçilebilir.</param>
    /// <param name="planeNormal">Düzlemin normali</param>
    /// <returns>Noktanın düzleme mesafesini döndürür.Matematiksel bir tabirle yansıma vektörünün büyüklüğünü döner</returns>
    /// <exception cref="ArgumentException">Düzlem ve doğrunun boyut kontrolünün uyuşmaması durumunda bu hatayı fırlatır</exception>
    public T DistanceToPlane(Matrix<T> planeOrigin, Matrix<T> planeNormal)
    {
        //Temel Mantık şu: Düzlemin orijininden noktaya çizilen vektörün düzlemin normali ile olan iç çarpımı noktanın düzleme en kısa uzaklığını verecektir.
        //Teme formül şudur ||proj vn || = |v.n| / ||n|| 

        //O.Adım: Boyut ve Hata kontrolü
        if (this.row != 3 || this.col != 1 || planeOrigin.row != 3 || planeOrigin.col != 1 || planeNormal.row != 3 || planeNormal.col != 1)
        {
            throw new ArgumentException("Mesafe hesabı için nokta, düzlem orijini ve düzlem normali 3x1 boyutunda birer vektör olmalıdır.");
        }
        //1.Adım: Orijinden noktaya çizilen vektörü oluşturalım
        Matrix<T> v = this - planeOrigin;
        //2.Adım: v vektörünü ile normali dot çarpım yapalım
        T dotProduct = Matrix<T>.Dot(v, planeNormal); //DİKKAT:Hesapladığımız v vektörü zaten hali hazırda düzlemin üzerindeyse normal ile olan iç çarpımından sıfır gelecektir.Çünkü normal yüzüye diktir! Nokta düzlem üzerinde demek ki normal ile olan çarpımı sıfırdır. Bu yüzden ekstra bir kontrol yapmana gerek yok.
        //3.Adım: dot product'ı normal uzunluğuna bölelim
        T distance = T.Abs(dotProduct) / planeNormal.Magnitude(); //Yönlü mesafe değil mutlak mesafeyi aradığımız için dotProdcut'ın mutlak değerini alıyoruz.Ayrıca uzaklık nasıl negatif olsun. Payda zaten büyüklük olduğu için pozitif gelir.
        return distance;
        //Not: Biz noktanın doğrunun önünde arkasında vs vs yani neresinde kaldığına göre distance aramıyoruz bu yüzden SignedDistance işaretli mesafe hesaplamamıza gerek yok.
    }

    /// <summary>
    /// Bir noktanın bir doğruya en kısa (dik) uzaklığını hesaplar.Nesnenin arrayinde tuttuğu noktaları noktanın koordinatları olarak alır.Sadece 3 boyutta işlem yapar.Sadece 3D için doğru sonuç verir.
    /// </summary>
    /// <param name="lineOrigin">Doğrunun başlangıç noktası</param>
    /// <param name="lineDirection">Doğrunun doğrultu vektörü(Direction Vector)</param>
    /// <returns>Noktanın düzleme mesafesini döndürür.Matematiksel bir tabirle Oluşan paralelkenarın yüksekliğini döner</returns>
    /// <exception cref="ArgumentException">Verilen metot argümanlarının ve nesnenin boyut kontrolünü yapar</exception>
    public T DistanceToLine(Matrix<T> lineOrigin, Matrix<T> lineDirection)
    {
        //Temel Mantık: Doğrunun başlangıç noktasından  noktaya çizdiğimiz vektör ile direction vektör arasında bir paralelkenar oluşur.Paralelkenarın alanı ise taban alanı çarpı yüksekliktir.Burada yükseklik aslında noktanın doğruya uzaklığını belirtir.O zaman,
        //Temel formül d= ||PQ X v|| / ||v||
        // Çapraz çarpımdan gelen paralelkenarın alanını direction vektörün büyüklüğüne bölersek yüksekliği yani noktanın doğruya en yakın uzaklığını buluruz.     
        // 0.Adım: Boyut ve Sıfır Vektör Kontrolleri
        if (this._values.Length != 3 || lineOrigin._values.Length != 3 || lineDirection._values.Length != 3)
        {
            throw new ArgumentException("Mesafe hesabı için nokta, doğru orijini ve yön vektörü 3 boyutlu olmalıdır.");
        }

        T dirMag = lineDirection.Magnitude();
        if (dirMag == T.Zero)
        {
            throw new ArgumentException("Doğrunun yön vektörü (lineDirection) sıfır vektörü olamaz.");
        }
        //1.Adım: Doğrunun başlangıç noktasından noktaya bir vektör çizelim ve PQ vektörü diyelim.P başlangıç Q ise noktayı temsil etsin
        Matrix<T> PQ = this - lineOrigin;
        //2.Adım: Çapraz çarpım ile  PQ ve line direction arasında kalan paralel kenarın alanını bulalım
        Matrix<T> area = PQ.Cross(lineDirection); //Çapraz çarpım sıfır ise nokta doğrunun üzerindedir ve nokta doğru üzerinde ise mesafesi sıfır çıkar. Bu yüzden herhangi bir ekstra kontrol yapmadım.
        //3.Adım: Paralelkenarın yüksekliğini yani noktanın doğruya en yakın mesafesini hesaplayalım
        T h = area.Magnitude() / dirMag;
        return h;
        //T h = area.Magnitude() / lineDirection.Magnitude();
    }
    /// <summary>
    /// Bir noktanın bir doğruya olan en kısa (dik) uzaklığının koordinatlarını verir.Çıkan nokta doğru üzerinde olmalıdır.Sadece 3D'de sonuç verir
    /// </summary>
    /// <param name="lineOrigin">Doğrunun başlangıç noktası</param>
    /// <param name="lineDirection">Doğrunun doğrultu vektörü(Direction Vector)</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Metot argümanları ve nesne için boyut kontrolü uyuşmazsa bu hatayı fırlatır.</exception>
    public Matrix<T> ProjectOnLine(Matrix<T> lineOrigin, Matrix<T> lineDirection)
    {
        //Temel Mantık: Doğrunun başlangıç noktasından noktaya bir vektör çizeceğim. Daha sonra bu vektörü direction vektör ile iç çarpıma sokacağım. Son olarak normalize ettiğim yön vektörü ile iç çarpımdan gelen değeri çarpıp return edeceğim.
        //0.Adım:Doğrunun başlangıç noktasından noktaya giden vektörü oluşturalım.
        if (this._values.Length != 3 || lineOrigin._values.Length != 3 || lineDirection._values.Length != 3)
            throw new ArgumentException("İzdüşüm hesabı için vektörler 3 boyutlu olmalıdır.");
        //1.Adım:Doğru başlangıcından noktaya bir vektör oluşturalım.
        Matrix<T> PQ = this - lineOrigin;
        //2.Adım:// 2. PQ'nun doğru yönü üzerindeki gölgesi (Bu bize origin'den ne kadar uzaklaştığımızı vektörel olarak verir)               
        Matrix<T> projectionVector = PQ.Project(lineDirection);//Dikkat:Bu vektör sana orijin noktasından ne kadar ilerlemen gerektiğini söyler. Bir alt satırda orijin noktasına bu vektörü eklememizin sebebi bu. Ben formulü orijin 0,0,0 imiş gibi türetmeyi biliyorum.
        //3. Adım: Başlangıç noktasına ne kadar ilerlediğimizi bulmak için projectionVector'u ekliyoruz.
        return lineOrigin + projectionVector;
        //DİKKAT: 3. adımı düşünemedim!
    }
    /// <summary>
    /// Bir noktanın bir düzleme olan en kısa (dik) uzaklığının koordinatlarını verir.Çıkan nokta düzlem üzerinde olmalıdır.Sadece 3D'de sonuç verir
    /// </summary>
    /// <param name="planeOrigin">Düzlemin orijinini temsil eden metot argümanı.Eilnde bir nokta yoksa düzlem denklemini sağlayan bir nokta seçilebilir</param>
    /// <param name="planeNormal">Düzlemin normalini temsil eden metot argümanı</param>
    /// <returns>Noktanın düzleme en yakın noktasının koordinatlarını döndürür</returns>
    /// <exception cref="ArgumentException">Metot argümanları ve nesne için boyut kontrolü uyuşmazsa bu hatayı fırlatır.</exception>
    public Matrix<T> ProjectOnPlane(Matrix<T> planeOrigin, Matrix<T> planeNormal)
    {
        //Temel Mantık şu:Temelde rejection vektörü bulacağız.Düzlemin orijininden notaya bir vektör çiz. Bu vektörün normal üzerine yansımasını(project) al.Yansımayı daha sonra normal ile çarpıp vektör haline getir
        // Noktadan bu vektörü çıkartırsan düzlem üzerindeki koordinatları bulursun.
        //Formul this - ((n.v)/(n.n)).n => n normal vektörü v ise plane orijinden noktaya çizilen vektör.
        //Dikkat:Nokta halihazırda düzlem üzerinde ise rejectionVector iç çarpımdan sıfır gelecek çünkü normal ile düzlem üzerindeki bir noktanın iç çarpımı sıfırdır. Cevap olarak this ile belirtilen nokta gelir.

        //0.Adım: Boyut kontrolü için gerekli kontrolleri yapan kodu yaz.Ama galiba DistanceToPlane'de yazdığımız için burada gerek yok galiba??
        // 0. Adım: Boyut Kontrolü (Bunu mutlaka kendi içinde de yapmalısın)
        if (this._values.Length != 3 || planeOrigin._values.Length != 3 || planeNormal._values.Length != 3)
        {
            throw new ArgumentException("Projeksiyon işlemi için tüm argümanlar 3 boyutlu vektör olmalıdır.");
        }

        // 1. Noktadan düzlemin orijinine giden eğik vektör
        Matrix<T> PQ = this - planeOrigin;

        // 2. Bu eğik vektörün, Düzlem Normali üzerine izdüşüm vektörü (Dik Bileşen / Rejection)
        Matrix<T> rejectionVector = PQ.Project(planeNormal);

        // 3. Orijinal noktadan, bu dik bileşeni çıkarırsak nokta düzlemin üzerine "düşer".Koordinatlar gelir.
        return this - rejectionVector;
    }

    /// <summary>
    /// Bir vektöre dik olan rastgele bir vektör üreten metot
    /// </summary>
    /// <returns>Nesneye dik olan vektör döndürür.</returns>
    public Matrix<T> GetOrthogonalVector()
    {
        //Temel Mantık şu:Rastgele bir vektör üretiyoruz. Bu rastgele vektörü elimizdeki vektör ile çapraz çarpıma sokuyoruz ve return ediyoruz. Çünkü çapraz çarpım iki vektöre dik üçüncü bir vektör üretir.
        //Milyarda bir ihtmal olmasının sebebi -100 ile 100 arasında bir rakam seçmemiz.
        Matrix<T> randomVector = new(1, 3);
        randomVector._values[0] = GetRandomNumber(T.CreateChecked(-100), T.CreateChecked(100));
        randomVector._values[1] = GetRandomNumber(T.CreateChecked(-100), T.CreateChecked(100));
        randomVector._values[2] = GetRandomNumber(T.CreateChecked(-100), T.CreateChecked(100));
        Matrix<T> result = this.Cross(randomVector);
        return result;
    }
    /// <summary>
    /// Ax = b sistemini çözerek x vektörünü/matrisini döndürür
    /// </summary>
    /// <param name="b">Lineer denklem sisteminde eşitliğin sağında kalan değişkeni temsil eden metot argümanı</param>
    /// <returns>Denklem sisteminin çözümünü temsil eden matrisi döndürür.</returns>
    public Matrix<T> Solve(Matrix<T> b)
    {
        //Temel Mantık şu: A matrisinin tersini DecomposeToLu() metodu ile tersini alacağım daha sonra aşırı yüklediğim * operatörü kullanara ile b'yi sağdan  ters matris ile çarpacağım.
        Matrix<T> inverse = this.DecomposeToLu();
        return b * inverse;
    }
    /// <summary>
    /// Matematiksel bir fonksiyonu matrisin tüm elemanlarına uygulayan metot.Mevcut matris üzerinde değişiklik yaparak çalışır herhangi bir şekilde matrisin kopyasını oluşturmaz!
    /// </summary>
    /// <param name="function">Matris elemanlarının her birine uygulanacak matematiksel fonksiyonu temsil eden metot argümanı</param>
    /// <returns>İşlem sonucu çıkan matrisi döndürür</returns>
    /// <exception cref="ArgumentNullException">Argüman olarak verilen fonksiyon null ise bu hatayı fırlatır.</exception>

    public Matrix<T> Map(Func<T, T> function)
    {
        // 0.Adım: Argüman olarak verilen function'ın null olup olmadğının kontrolünü yapalım.Yoksa null hatası fırlatır.
        if (function == null)
        {
            throw new ArgumentNullException(nameof(function), "Map işlemi için bir fonksiyon verilmelidir.");
        }
        for (int i = 0; i < this._values.Length; i++)
        {
            this._values[i] = function(this._values[i]);
        }
        return this;

    }
    /// <summary>
    /// Matematiksel bir fonksiyonu matrisin tüm elemanlarına uygulayan metot.Mevcut matris üzerinde değişiklik yapmaz.Mevcut matrisin bir kopyasını oluşturarak işlem yapar. 
    /// </summary>
    /// <param name="function">Matris elemanlarının her birine uygulanacak matematiksel fonksiyonu temsil eden metot argümanı</param>
    /// <returns>İşlem sonucu çıkan yeni bir matris döndürür</returns>
    /// <exception cref="ArgumentNullException">Argüman olarak verilen fonksiyon null ise bu hatayı fırlatır</exception>
    public Matrix<T> MapCopy(Func<T, T> function)
    {
        // 0.Adım: Argüman olarak verilen function'ın null olup olmadğının kontrolünü yapalım.Yoksa null hatası fırlatır. 
        if (function == null)
        {
            throw new ArgumentNullException(nameof(function), "Map işlemi için bir fonksiyon verilmelidir.");
        }
        //1.Adım: function null değilse kopyalama işlemi yapabiliriz.
        Matrix<T> copy = this.Copy();
        //1.Adım: DRY prensibine uymak için önceden yazdığım kodu çağırdım
        return copy.Map(function);
    }


    #region Static Metotlar
    internal static T GetRandomNumber<T>(T min, T max) where T : INumber<T>
    {
        //0.Adım:Argüman kontrolü.Min değer max değerden büyük olursa hata fırlatıyoruz.
        if (min > max)

            throw new ArgumentException("Minimum değer maksimum değerden büyük olamaz");

        // 1.Adım: Sıfır ile bir arasında rastgele bir sayı üret
        double scale = _random.NextDouble();
        // 2. Argümanları double'a çeviripi accuracy'i arttır
        double minAsDouble = Convert.ToDouble(min);
        double maxAsDouble = Convert.ToDouble(max);
        // 3. Rastgele sayıyı double formatında hesapla.Temelde max-min yapıp range buluyoruz ve bunu ölçeklendirip min'e ekliyoruz
        double randomDouble = minAsDouble + (scale * (maxAsDouble - minAsDouble));
        // 4. Elimizdeki dobule sayısını generic type T'ye dönüştürüp returnlüyoruz.
        return T.CreateChecked(randomDouble);

    }
    static public Matrix<T> ProjectOnPlane(Matrix<T> pointOrigin, Matrix<T> planeOrigin, Matrix<T> planeNormal)
    {
        return pointOrigin.ProjectOnPlane(planeOrigin, planeNormal);
    }
    public static Matrix<T> ProjectOnLine(Matrix<T> pointOrigin, Matrix<T> lineOrigin, Matrix<T> lineDirection)
    {
        return pointOrigin.ProjectOnLine(lineOrigin, lineDirection);
    }

    public static bool IntersectLineWithLine(Matrix<T> line1Origin, Matrix<T> line1Dir, Matrix<T> line2Origin, Matrix<T> line2Dir, out Matrix<T> intersectionPoint)
    {
        return line1Origin.IntersectLineWithLine(line1Dir, line2Origin, line2Dir, out intersectionPoint);
    }
    //Doğru ile düzlemin kesişip kesişmediğini bulan static metot
    public static bool IntersectLineWithPlane(Matrix<T> line1Origin, Matrix<T> lineDir, Matrix<T> planeOrigin, Matrix<T> planeNormal, out Matrix<T> intersectionPoint)
    {
        return line1Origin.IntersectLineWithPlane(lineDir, planeOrigin, planeNormal, out intersectionPoint);
    }

    //Hadamard Çarpımının diğer ismi Elementwise product.Bu isim için delegate atama işlemi yaptık.
    static public Matrix<T> ElementwiseProduct(Matrix<T> A, Matrix<T> B)
    {
        return HadamardProduct(A, B);
    }
    //Bir A matrisini uzun uzun A.Scalar(5) yazmak yerine * operatörünü bir daha aşırı yükleyerek A*5 işlemini yapılabilmesini sağlayan metot.
    static public Matrix<T> operator *(Matrix<T> A, T scalar)
    {
        Matrix<T> copy = A.Copy();
        for (int i = 0; i < copy._values.Length; i++)
        {
            copy._values[i] *= scalar;
        }
        return copy;

    }
    //Sadece A*5 değil 5*A yazmamızı da sağlayan metot.
    static public Matrix<T> operator *(T scalar, Matrix<T> A)
    {
        return A * scalar;
    }

    //Vektör izdüşümünü bulan static metot.A vektörünün B vektörüne izdüşüm vektörünü bulur

    static public Matrix<T> OuterProduct(Matrix<T> A, Matrix<T> B)
    {
        return A.OuterProduct(B);
    }
    static public Matrix<T> HadamardProduct(Matrix<T> A, Matrix<T> B)
    {
        return A.HadamardProduct(B);
    }
    static public T ScalarTriple(Matrix<T> A, Matrix<T> B, Matrix<T> C)
    {
        return A.ScalarTriple(B, C);
    }
    static public Matrix<T> CrossNormalized(Matrix<T> A, Matrix<T> B)
    {
        return A.CrossNormalized(B);
    }
    static public T Cross2D(Matrix<T> A, Matrix<T> B)
    {
        return A.Cross2D(B);
    }
    //Bu metot A-B-C noktalarını alır AB ve AC vektörlerini oluşturup ABxAC işlemini yaparak yeni vektör döndürür.
    /// <summary>
    /// A-B-C noktalarından bir yüzey oluşturur ve bu yüzeye dik olan vektörünü cross product yaparak hesaplar.Sırasıyla AB ve AC vektörlerini hesaplayıp çalışır.Normal vektörü olduğu için vektörün default olarak büyüklüğünü bir yapar.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <param name="C"></param>
    /// <param name="normalize">Default değeri true'dur.Bu sayede vektörün büyüklüğü her zaman bir olarak döner.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    static public Matrix<T> CalcSurfaceNormal(Matrix<T> A, Matrix<T> B, Matrix<T> C, bool normalize=true)
    {
        //0.Adım: Edge Case kontrolü:
        if (A == null || B == null || C == null)
        {
            throw new ArgumentNullException("Yüzey normali hesaplamak için verilen noktalar null olamaz.");
        }

        //1.Adım:Önce vektörleri oluşturalım.
        Matrix<T> AB = B - A; //AB vektörü B-A yaparak elde edilir.
        Matrix<T> AC = C - A; //Aynı şekilde AC vektörü C-A ile oluşturduk.
        //Burada tekrardan boyut kontrolü yapmıyorum çünkü çağıracağım  public Matrix<T> Cross(Matrix<T> B) imzalı metot içerisinde zaten kontrol var
        //2.Adım: Yüzey normalini hesaplayalım
        Matrix<T> normal = AB.Cross(AC);
        //3.Adım: normal vektörünü normalize edelim yani yönü tutup uzunluğuğ bir yapalım.
        return normalize ? normal.GetNormalized() : normal;  
        

    }
    static public Matrix<T> Cross(Matrix<T> A, Matrix<T> B)
    {
        return A.Cross(B); //Dry prensibini uygulamak için bunu da senden öğrenerek bu metodu yazdım :))
    }
    static public Matrix<T> Reject(Matrix<T> A, Matrix<T> B)
    {
        return A.Reject(B);
    }
    static public T ScalarProjection(Matrix<T> A, Matrix<T> B)
    {
        return A.ScalarProjection(B);
    }
    static public Matrix<T> Project(Matrix<T> A, Matrix<T> B)
    {
        return A.Project(B);
    }
    static public T Angle(Matrix<T> A, Matrix<T> B)
    {
        return A.Angle(B);
    }
    static public T Dot(Matrix<T> A, Matrix<T> B)
    {
        return A.Dot(B);
    }
    public static Matrix<T> GenerateIdentity(int size) //Bu metodu static yapmak ne kadar doğru emin değilim ?? Cevap. Sıfırdan yeni bir matris oluşturmak o andaki yeni matrisin verilerine ihtiyaç duymayacak.Bu yüzden bu metot static olabilir. 
    {
        Matrix<T> result = new(size, size);

        for (int i = 0; i < size; i++)
        {
            result[i, i] = T.One; // 1 ataması T.One ile yapılır.Diagonal dışındaki bütün değerleri sıfır atandı.
        }
        return result;
    }
    public static Matrix<T> GenerateZeroMatrix(int row, int column)
    {

        return new Matrix<T>(row, column); //Varsayılan olarak zaten sıfırlar ile dolu geldiği için direkt yeni bir matris döndürebilirsin
    }
    public static void Print(Matrix<T> A)
    {
        A.Print();// // String birleştirme hatasından kaçınmak için sınıf içindeki Print'i çağırıyoruz. 
    }
    //public static Matrix<T> GenerateRandomMatrix(int row, int column)
    //{
    //    Matrix<T> result = new(row, column);

    //    Random rand = new();

    //    for (int i = 0; i < result.TotalElements; i++)
    //    {
    //        //CreateCheked tam olarak ne işe yarıyor ?? Cevap: Üretilen değerin tipinden bir instance yaratıyor. Yani aslında sen bir sayı olabilirsin diyor. 
    //        result._values[i] = T.CreateChecked(rand.NextDouble()); // 0.0 ile 1.0 arasında değer üretir
    //    }
    //    return result;
    //}
    static public Matrix<T> operator +(Matrix<T> A, Matrix<T> B)
    {
        //Önce boyut kontrolü

        if (A.row != B.row || A.col != B.col)
        {
            throw new InvalidOperationException(
             $"Boyut uyuşmazlığı: İlk matris ({A.row}x{A.col}), " +
             $"İkinci matris ({B.row}x{B.col}) boyutlarında."
         );
        }

        Matrix<T> result = new(A.row, A.col); //Sonuç olarak döndürelecek matrisi yarattık

        for (int i = 0; i < A._values.Length; i++) //GetLentgh metodu sınıfta fields olarak bulunan row*cols işleminin sonucunu getiriyor.Gerçekten böyle bir metot gerekli mi ??
        {
            result._values[i] = A._values[i] + B._values[i]; //Acaba sadece A._values[i]+=B._values[i]; şeklinde bir satır mı yazsam. Ama o zaman A matrixini kaybediyoruz ??
        }
        return result;
    }
    static public Matrix<T> operator -(Matrix<T> A, Matrix<T> B)
    {
        //Önce boyut kontrolü

        if (A.row != B.row || A.col != B.col)
        {
            throw new InvalidOperationException(
             $"Boyut uyuşmazlığı: İlk matris ({A.row}x{A.col}), " +
             $"İkinci matris ({B.row}x{B.col}) boyutlarında."
         );
        }

        Matrix<T> result = new(A.row, A.col); //Sonuç olarak döndürelecek matrisi yarattık

        for (int i = 0; i < A._values.Length; i++) //GetLentgh metodu sınıfta fields olarak bulunan row*cols işleminin sonucunu getiriyor.Gerçekten böyle bir metot gerekli mi ??
        {
            result._values[i] = A._values[i] - B._values[i]; //Acaba sadece A._values[i]+=B._values[i]; şeklinde bir satır mı yazsam. Ama o zaman A matrixini kaybediyoruz ??
        }
        return result;
    }
    static public Matrix<T> operator *(Matrix<T> A, Matrix<T> B)
    {
        //Öncelikle matrixler boyut olarak uyumlı mu diye kontrol yapmamız lazım.
        if (A.col != B.row)
        {
            throw new InvalidOperationException(
            $"Boyut uyuşmazlığı: İlk matris ({A.row}x{A.col}), " +
            $"İkinci matris ({B.row}x{B.col}) boyutlarında.");
        }

        Matrix<T> result = new(A.row, B.col);
        for (int i = 0; i < A.row; i++)
        {
            for (int j = 0; j < B.col; j++)
            {
                // PERFORMANS İPUCU: Değeri sürekli matrise yazmak (result[i,j] += ...) yavaştır.
                // Bunun yerine geçici bir değişkende toplayıp en son matrise atamak çok daha hızlıdır.
                T sum = T.Zero;
                for (int k = 0; k < A.col; k++) //Cevabın sütunlarını dönmeyi temsil ediyor.Çok ama çok zekice.
                {
                    sum += A[i, k] * B[k, j];
                }
                result[i, j] = sum;
            }
        }
        return result;
    }


    /// <summary>
    /// İki matrisin aynı olup olmadığını kontrol eden metot. Basamak hassasiyeti için default olarak 9 basamak kullanır. Hassasiyet epsilon değeri verilerek değiştirilebilir.
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    //static public bool AreIdentical(Matrix<T> A, Matrix<T> B)
    //{
    //    return AreIdentical(A, B, epsilon);
    //}
    //static public bool AreIdentical(Matrix<T> A, Matrix<T> B, T epsilon)
    //{
    //    if (A.row != B.row || A.col != B.col) //Matrislerin boyut kontrolü.
    //    {
    //        return false;
    //    }
    //    //Matrislerin element-wise kontrolü. 
    //    for (int i = 0; i < A._values.Length; i++)
    //    {
    //        if (T.Abs(A._values[i] - B._values[i]) > epsilon)
    //        {
    //            return false;
    //        }
    //    }
    //    return true;
    //}

    /// <summary>
    /// İki matrisin dot productını bulan static metot. 
    /// </summary>
    /// <param name="A"></param>
    /// <param name="B"></param>
    /// <returns></returns>
    /// 

    //NOT: Çalışma zamanının hızlanması için bilerek çoğu metodu yorum haline getirdim.

    //public static Matrix<T> ScaleMatrix(Matrix<T> A, T scaleFactor)
    //{
    //    //Ölçeklendirmede herhangi bir kontrol yapmamız lazım mı ?? Görebildiğim kadarıyla yok.Indexer ile değil direkt olarak values ile döndürmemiz lazım galiba
    //    Matrix<T> result = new(A.row, A.col);
    //    for (int i = 0; i < A._values.Length; i++)
    //    {
    //        result._values[i] = A._values[i] * scaleFactor;
    //    }
    //    return result;
    //}

    #endregion

}

//Yapılacaklar Listesi
//Verilen bir matrisin karesini ve verilen matrisin n'nci kuvvetini bulamyı kodlamamız lazım.
//Float matristen double'a double'dan da float'a döndüren metodu yazmamız lazım. Jnerik kullanarak yaptığımız için biraz ilginç durum ve durumlar ortaya çıkabilir.
//LowerTriangular matris halini de metotların içerisine koymam gerekiyor. PublicDecomposeToLU matrisi hem lower-upper triangle ve determinantı geri döndürmeli.


#region Öğrendiklerim
//  static public Matrix<T> operator *(Matrix<T> A, Matrix<T> B, bool leftProdcut). *,-,+ gibi operatörler ikili operatörler olduğu için bu operatörleri aşırı yüklerken metot argümanı olarak sadece iki değer verebilirsin.
//Burada bahsettiğim şekilde üçüncü bir bool leftProduct gibi bir argüman vermek mümkün değildir.
//Generic Math kullandığım için aslında math sınıfı ile yapabileceğim galiba her şeyi T ile de yapabiliyorum.
//Birim matris tanımlayarak da işlem yapabilirsin.Çarpma işleminde geçici bir değişken olarak tanımlayıp kullandık.
//Tüm değerleri x olan nxm matris üreten metodu yaz.Yapıcı metot da olabilir.
//Guard Clauses kavramı: Matris elemanlarının kuvvetini almaya çalışırken karşılaştığım bir kavram. return getirebilecek  koşul bloklarını en üste yazarak diğer işlem yapılması gereken if bloklarının çalışmasının önüne geçiyoruz.
//Gauss-Jordan yönteminde pivotların üstündeki değerleri sıfırlarken satırları swap etme işlemi yapılamaz.Bu yüzden de yuvarlama hatası round-off erorr ortaya çıkabiliyor.0.00000...1 gibi bir sayıyı 5000000 gibi büyük bir sayıya bölme durumu oluşabilir.
//Static bir metot eklemek static değişken eklemek gibi performans kaybı yaratmaz! Unutma metotlar özünde bir talimattır.
//T.Clamp() metodu yardımı ile bir değeri istediğin iki değer arasına çekebilirsin. İki vektör arasındaki açıyı bulurken kullandım.Cos değerini -1,1 arasın çektik.
// Bir işlem birden fazla objeyi ilgilendiriyorsa hem nesneye yönelik hem de static versiyonu sunulur. Bu temel prensipten dolayı static metotları kodladım.
//Hadamard Çarpımının diper adı olan elementwise product için hem static hem de normal metot olarak delegate ekledim.Bu sayede çarpım her iki isimle de çağrılabiliyor.
#endregion

#region Öğrenmem Gerekenler
//C++ dili ile yazarken Gauss-Jordan ve Decomposition'ın Uzay zaman karşılaştırması ve neden LU decomposition tercih ediliyor.
//C++'a geçince kareleyerek Üs alma algoritmasını öğrenmelisin.Exponentiation by Squaring.Negatif kuvvetleri de alan bir T Pow fonksiyonunu kütüphaneye eklemen lazım.
//Unit test ne? Nasıl yapılıyor ?? Kütüphaneyi unit testlere tabi tutarak performansını ve doğruluğunu kontrol etmem lazım.
#endregion



