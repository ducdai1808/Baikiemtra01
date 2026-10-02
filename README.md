# Baikiemtra01
Bài kiểm tra số 1 -C# .NET
## Thông tin sinh viên
-Họ và tên: Đỗ Đức Đại

-Mã sinh viên:24810310244

-LớpD19CNPM3

## Câu 1 : Bài lí thuyết 
Câu 1: Phân biệt Value Types và Reference Types

Value Types (kiểu giá trị) lưu trực tiếp giá trị của biến. Các kiểu thường gặp gồm int, float, bool, struct, enum. Khi gán một biến Value Type cho biến khác thì giá trị được sao chép sang biến mới. Trong nhiều trường hợp, biến cục bộ được lưu trên Stack.

Reference Types (kiểu tham chiếu) lưu một tham chiếu đến đối tượng. Các kiểu thường gặp gồm class, array, string, object. Đối tượng thường được lưu trên Heap, còn biến tham chiếu chứa địa chỉ/tham chiếu đến đối tượng.
Ví dụ:
int a = 10;
int b = a;
b = 20;

Sau khi b = 20 thì a vẫn bằng 10 vì a và b là hai giá trị độc lập.

Với Reference Type:

Student s1 = new Student();
Student s2 = s1;

s1 và s2 cùng tham chiếu đến một đối tượng trên Heap, nên thay đổi đối tượng thông qua s2 có thể làm thay đổi dữ liệu mà s1 nhìn thấy.

Tóm lại: Value Type lưu giá trị, còn Reference Type lưu tham chiếu đến đối tượng.

Câu 2: Sự khác nhau giữa init và set

set cho phép thuộc tính được thay đổi sau khi đối tượng đã được tạo.

class Student
{
    public string Name { get; set; }
}

Student s = new Student();
s.Name = "Dai";
s.Name = "Nam"; // Được phép

init chỉ cho phép thiết lập giá trị khi khởi tạo đối tượng hoặc trong constructor. Sau khi đối tượng được khởi tạo thì không thể thay đổi giá trị đó.

class Student
{
    public string Name { get; init; }
}

Student s = new Student
{
    Name = "Dai"
};

// s.Name = "Nam"; // Lỗi

Trường hợp sử dụng thực tế: init phù hợp với những thuộc tính không nên thay đổi sau khi tạo đối tượng, chẳng hạn như mã tài khoản, mã sinh viên hoặc mã sản phẩm.

Tóm lại: set có thể thay đổi sau khi khởi tạo, còn init chỉ thiết lập giá trị trong quá trình khởi tạo.

Câu 3: Phân biệt virtual và override

virtual được khai báo trong lớp cha, cho phép lớp con ghi đè phương thức đó.

class Animal
{
    public virtual void Sound()
    {
        Console.WriteLine("Animal sound");
    }
}

override được khai báo trong lớp con, dùng để cung cấp cách triển khai mới cho phương thức virtual của lớp cha.

class Dog : Animal
{
    public override void Sound()
    {
        Console.WriteLine("Gau gau");
    }
}

Khi sử dụng:

Animal a = new Dog();
a.Sound();

Kết quả:

Gau gau

Điều này thể hiện tính đa hình (Polymorphism): biến có kiểu Animal nhưng khi chạy lại thực hiện phương thức Sound() của lớp Dog.

Tóm lại:

virtual: lớp cha cho phép phương thức được ghi đè.
override: lớp con ghi đè phương thức của lớp cha.
virtual và override được sử dụng để triển khai tính đa hình trong C#.
Câu 4: Tại sao static không thể truy xuất thông qua Object Instance?

Thành phần được khai báo static thuộc về Class, không thuộc về từng Object Instance.

Ví dụ:

class Student
{
    public static int Count = 0;
}

Count là thành viên static, vì vậy chỉ có một biến Count dùng chung cho toàn bộ lớp Student.

Có thể truy xuất bằng tên lớp:

Student.Count++;

Trong khi đó:

Student s = new Student();

là tạo một Object Instance riêng. Object này không sở hữu riêng thành viên static.

Lý do: static thuộc về Class, còn các thành viên thông thường thuộc về từng Object Instance.

Tóm lại: Thành viên static được truy cập thông qua tên Class, không thông qua Object Instance.
## Câu 2 bài thực hành 
