using System;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Xml.Linq;

public class Students
{
    public string MaSoSinhVien { get; set; }
    public string TenSinhVien { get; set; }
    public string Email { get; set; }
    public double GPA { get; set; }
    public string XepLoai { get; set; }

    public DateTime NgaySinh;

    public string DiaChi;

    public string SoDienThoai;

}
public class Courses
{
    public string TenMonHoc { get; set; }
    public string MaMonHoc { get; set; }
    public string GiaoVien { get; set; }
    public DateTime NgayBatDau { get; set; }
}

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
public static class BaiTapQuanLySinhVien
{
    static List<User> users = new List<User>();


    static void Main(string[] args)
    {
        users.Add(new User("admin", "1234"));
        Console.OutputEncoding = Encoding.UTF8;
        while (true)
        {
            Console.WriteLine("\t1. Đăng nhập");
            Console.WriteLine("\t2. Tạo tài khoản");
            Console.WriteLine("\t3. Thoát");
            Console.Write("Vui lòng chọn một tùy chọn: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                    break;
            }
        }
    }

    static void Login()
    {
        Console.Write("Nhập Tài khoản (email/mã số sinh viên/điện thoại): ");
        string username = Console.ReadLine();
        Console.Write("Nhập Mật khẩu: ");
        string password = Console.ReadLine();

        // Kiểm tra đăng nhập
        User user = users.Find(u => u.Username == username);
        if (user != null)
        {
            if (user.Password == password)
            {
                Console.WriteLine("Đăng nhập thành công!");
                QuanLySinhVien.TrangChinh();
            }
            else
            {
                Console.WriteLine("Sai mật khẩu. Vui lòng kiểm tra lại.");
            }
        }
        else
        {
            Console.WriteLine("Tài khoản không tồn tại. Vui lòng kiểm tra lại.");
        }
    }

    static void Register()
    {
        Console.Write("Nhập Tài khoản (email/mã số sinh viên/điện thoại): ");
        string username = Console.ReadLine();
        Console.Write("Nhập Mật khẩu: ");
        string password = Console.ReadLine();

        // Kiểm tra và lưu tài khoản
        bool isPasswordValid = IsStrongPassword(password);
        bool isUsernameValid = IsValidUsername(username);

        if (isPasswordValid && isUsernameValid)
        {
            // Kiểm tra xem tài khoản đã tồn tại chưa
            if (users.Exists(u => u.Username == username))
            {
                Console.WriteLine("Tài khoản đã tồn tại.");

            }
            else
            {
                // Lưu tài khoản và mật khẩu vào danh sách
                users.Add(new User(username, password));
                Console.WriteLine("Tạo tài khoản thành công!");
                Login();
            }
        }
        else
        {
            Console.WriteLine("Tạo tài khoản thất bại. Vui lòng kiểm tra lại thông tin đăng ký.");
            Console.WriteLine("** Mật khẩu bắt buộc phải có ký tự đặc biệt:'@,#,$,...");
            Console.WriteLine("** Mật khẩu bắt buộc phải có chữ in hoa");
            Console.WriteLine("** Hoặc kiểm tra xem tài khoản có đúng dạng kiểu email, số điện thoại (có 10 chữ số) hoặc mã số sinh viên (11 chữ số) ");
            Console.WriteLine("===============================================================");
        }
    }

    static bool IsStrongPassword(string password)
    {
        // Kiểm tra mật khẩu đủ mạnh (chứa ký tự đặc biệt, số, chữ in hoa)
        bool hasSpecialCharacter = new Regex(@"[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]").IsMatch(password);
        bool hasNumber = new Regex(@"[0-9]").IsMatch(password);
        bool hasUpperCase = new Regex(@"[A-Z]").IsMatch(password);

        return hasSpecialCharacter && hasNumber && hasUpperCase;
    }


    static bool IsValidUsername(string username)
    {
        // Kiểm tra xem tài khoản có đúng định dạng email, số điện thoại hoặc mã số sinh viên
        bool isEmail = new Regex(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$").IsMatch(username);
        bool isPhoneNumber = new Regex(@"^\d{10}$").IsMatch(username); // Ví dụ cho số điện thoại 10 chữ số
        bool isStudentID = new Regex(@"^\d{11}$").IsMatch(username); // Ví dụ cho mã số sinh viên 11 chữ số

        return isEmail || isPhoneNumber || isStudentID;
    }
}



public class QuanLySinhVien
{
    static List<Courses> course = new List<Courses>(); //Bien toan cuc bai Khanh

    static List<Students> student = new List<Students>(); //Bien toan cuc bai Long

    public static void TrangChinh()
    {
        Console.InputEncoding = Encoding.Unicode;
        Console.OutputEncoding = Encoding.UTF8;

        while (true)
        {
            Console.Clear();
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.WriteLine("============================================ QUẢN LÝ SINH VIÊN / MÔN HỌC ============================================");
            Console.WriteLine("\t1. Danh sách môn học");
            Console.WriteLine("\t2. Danh sách sinh viên");
            Console.WriteLine("\t3. Tim Kiếm sinh Viên");
            Console.WriteLine("\t4. Tìm Kiếm môn học");
            Console.WriteLine("\t5. Thêm môn học");
            Console.WriteLine("\t6. Thêm sinh viên");
            Console.WriteLine("\t7. Đăng ký Môn học");
            Console.WriteLine("\t8. Tính xếp Loại Sinh Viên");
            Console.WriteLine("\t0. Thoát chương trình");
            Console.Write("Vui lòng chọn chức năng thực hiện: ");
            string choice = Console.ReadLine();
            Console.WriteLine("========================================================================================");
            switch (choice)
            {
                case "1":
                    {
                        HienThiDanhSachMonHoc();
                    }
                    break;
                case "2":
                    {
                        HienThiDanhSachSinhVien();
                    }
                    break;

                case "3":
                    {
                        TimKiemSinhVien();
                    }
                    break;
                case "4":
                    {
                        TimKiemMonHoc();
                    }
                    break;
                case "5":
                    {
                        ThemMonHoc();
                    }
                    break;
                case "6":
                    {
                        ThemSinhVien();
                    }
                    break;
                case "7":
                    {
                        DangKyMonHoc();
                    }
                    break;
                case "8":
                    {
//                        TinhXepLoai();
                    }
                    break;
                case "0":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng thử lại.");
                    continue;
            }
            Console.ReadKey();
        }
    }
    public static void HienThiDanhSachMonHoc()
    {
        Console.Clear();
        string filePath = "subjects.txt"; // Thay thế đường dẫn bằng đường dẫn thực tế của tệp văn bản
        List<Courses> courses = new List<Courses>();
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                Courses currentCourse = null;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        if (currentCourse == null)
                        {
                            currentCourse = new Courses();
                            courses.Add(currentCourse);
                        }

                        switch (key)
                        {
                            case "TenMonHoc":
                                currentCourse.TenMonHoc = value;
                                break;
                            case "MaMonHoc":
                                currentCourse.MaMonHoc = value;
                                break;
                            case "GiaoVien":
                                currentCourse.GiaoVien = value;
                                break;
                            case "NgayBatDau":
                                if (DateTime.TryParse(value, out DateTime ngayBatDau))
                                {
                                    currentCourse.NgayBatDau = ngayBatDau;
                                }
                                else
                                {
                                    Console.WriteLine($"Lỗi: Không thể chuyển đổi ngày {value}.");
                                }
                                currentCourse = null; // Kết thúc thông tin của sinh viên, sẵn sàng cho sinh viên tiếp theo
                                break;
                        }
                    }
                }
            }
            int itemsPerPage = 10; // Số lượng sinh viên hiển thị trên mỗi trang
            int currentPage = 0; // Trang hiện tại

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\tDanh sách môn học (Trang {0}):", currentPage + 1);
                Console.WriteLine();
                Console.WriteLine("{0,-40} {1,-25} {2,-30} {3,-15}", "Tên môn học", "Mã môn học", "Giáo viên", "Ngày bắt đầu");
                Console.WriteLine("==============================================================================================================================================");

                for (int i = currentPage * itemsPerPage; i < (currentPage + 1) * itemsPerPage && i < courses.Count; i++)
                {
                    var course = courses[i];
                    Console.WriteLine("{0,-40} {1,-25} {2,-30} {3,-15}", course.TenMonHoc, course.MaMonHoc, course.GiaoVien, course.NgayBatDau.ToString("dd/MM/yyyy"));
                    Console.WriteLine("==============================================================================================================================================");
                }

                Console.WriteLine("Ấn 'N' để xem trang tiếp theo, 'P' để xem trang trước, hoặc 'Q' để thoát.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.N && (currentPage + 1) * itemsPerPage < courses.Count)
                {
                    currentPage++;
                }
                else if (keyInfo.Key == ConsoleKey.P && currentPage > 0)
                {
                    currentPage--;
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    TrangChinh();
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi: " + e.Message);
        }

    }
    public static void HienThiDanhSachSinhVien()
    {
        Console.Clear();
        string filePath = "students.txt"; // Thay thế đường dẫn bằng đường dẫn thực tế của tệp văn bản
        List<Students> students = new List<Students>();

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                Students currentStudent = null;

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        if (currentStudent == null)
                        {
                            currentStudent = new Students();
                            students.Add(currentStudent);
                        }

                        switch (key)
                        {
                            case "MaSoSinhVien":
                                currentStudent.MaSoSinhVien = value;
                                break;
                            case "TenSinhVien":
                                currentStudent.TenSinhVien = value;
                                break;
                            case "Email":
                                currentStudent.Email = value;
                                break;
                            case "GPA":
                                if (double.TryParse(value, out double gpa))
                                {
                                    currentStudent.GPA = gpa;
                                }
                                break;
                            case "XepLoai":
                                currentStudent.XepLoai = value;
                                currentStudent = null; // Kết thúc thông tin của sinh viên, sẵn sàng cho sinh viên tiếp theo
                                break;
                        }
                    }
                }
            }

            // Hiển thị danh sách sinh viên sau khi đọc từ tệp
            int itemsPerPage = 10; // Số lượng sinh viên hiển thị trên mỗi trang
            int currentPage = 0; // Trang hiện tại

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\tDanh sách sinh viên (Trang {0}):", currentPage + 1);
                Console.WriteLine();
                Console.WriteLine("{0,-25} {1,-35} {2,-35} {3,-15} {4,-20}", "Mã số sinh viên", "Tên sinh viên", "Email", "GPA", "Xếp Loại");
                Console.WriteLine("==============================================================================================================================================");

                for (int i = currentPage * itemsPerPage; i < (currentPage + 1) * itemsPerPage && i < students.Count; i++)
                {
                    var student = students[i];
                    Console.WriteLine("{0,-25} {1,-35} {2,-35} {3,-15} {4,-20}", student.MaSoSinhVien, student.TenSinhVien, student.Email, student.GPA, student.XepLoai);
                    Console.WriteLine("==============================================================================================================================================");
                }

                Console.WriteLine("Ấn 'N' để xem trang tiếp theo, 'P' để xem trang trước, hoặc 'Q' để thoát.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.N && (currentPage + 1) * itemsPerPage < students.Count)
                {
                    currentPage++;
                }
                else if (keyInfo.Key == ConsoleKey.P && currentPage > 0)
                {
                    currentPage--;
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    TrangChinh();
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi: " + e.Message);
        }
    }
    public static void TimKiemSinhVien()
    {
        Console.Clear();
        string filePath = "students.txt"; // Thay thế đường dẫn bằng đường dẫn thực tế của tệp văn bản
        List<Students> students = new List<Students>();

        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                Students currentStudent = null;

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        if (currentStudent == null)
                        {
                            currentStudent = new Students();
                            students.Add(currentStudent);
                        }

                        switch (key)
                        {
                            case "MaSoSinhVien":
                                currentStudent.MaSoSinhVien = value;
                                break;
                            case "TenSinhVien":
                                currentStudent.TenSinhVien = value;
                                break;
                            case "Email":
                                currentStudent.Email = value;
                                break;
                            case "GPA":
                                if (double.TryParse(value, out double gpa))
                                {
                                    currentStudent.GPA = gpa;
                                }
                                break;
                            case "XepLoai":
                                currentStudent.XepLoai = value;
                                currentStudent = null; // Kết thúc thông tin của sinh viên, sẵn sàng cho sinh viên tiếp theo
                                break;
                        }
                    }
                }
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi: " + e.Message);
        }
        Console.WriteLine("** Dựa trên MSSV or Tên SV or Email or GPA or Xếp Loại:");
        Console.Write("Nhập từ khoá tìm kiếm:");
        string keyword = Console.ReadLine();
        List<Students> KetQuaTimKiem = TimKiemSinhVien(students, keyword);

        // Hiển thị kết quả tìm kiếm
        Console.WriteLine("Kết quả tìm kiếm:");
        if (KetQuaTimKiem.Count != 0)
        {
            int itemsPerPage = 10; // Số lượng sinh viên hiển thị trên mỗi trang
            int currentPage = 0; // Trang hiện tại
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\tDanh sách tìm kiếm (Trang {0}):", currentPage + 1);
                Console.WriteLine();
                Console.WriteLine("{0,-25} {1,-35} {2,-35} {3,-15} {4,-20}", "Mã số sinh viên", "Tên sinh viên", "Email", "GPA", "Xếp Loại");
                Console.WriteLine("==============================================================================================================================================");
                for (int i = currentPage * itemsPerPage; i < (currentPage + 1) * itemsPerPage && i < KetQuaTimKiem.Count; i++)
                {
                    var student = KetQuaTimKiem[i];
                    Console.WriteLine("{0,-25} {1,-35} {2,-35} {3,-15} {4,-20}", student.MaSoSinhVien, student.TenSinhVien, student.Email, student.GPA, student.XepLoai);
                    Console.WriteLine("==============================================================================================================================================");
                }

                Console.WriteLine("Ấn 'N' để xem trang tiếp theo, 'P' để xem trang trước, hoặc 'Q' để thoát.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.N && (currentPage + 1) * itemsPerPage < KetQuaTimKiem.Count)
                {
                    currentPage++;
                }
                else if (keyInfo.Key == ConsoleKey.P && currentPage > 0)
                {
                    currentPage--;
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("\t \t Không tìm thấy sinh viên");
        }
        Console.WriteLine("=========================================");
        Console.WriteLine("\t1. Tìm Kiếm Tiếp");
        Console.WriteLine("\t2. Quay Lại Trang Chính");
        Console.WriteLine("\t3. Thoát");
        Console.Write("Vui lòng chọn chức năng thực hiện: ");
        while (true)
        {
            string choice2 = Console.ReadLine();
            if (choice2 == "1" || choice2 == "2" || choice2 == "3")
                switch (choice2)
                {
                    case "1":
                        TimKiemSinhVien();
                        break;
                    case "2":
                        TrangChinh();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            else Console.WriteLine("Chức năng không tồn tại, vui lòng chọn lại chức năng thực hiện: ");
        }
    }

    public static void TimKiemMonHoc()
    {
        Console.Clear();
        string filePath = "subjects.txt"; // Thay thế đường dẫn bằng đường dẫn thực tế của tệp văn bản
        List<Courses> courses = new List<Courses>();
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                Courses currentCourse = null;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        if (currentCourse == null)
                        {
                            currentCourse = new Courses();
                            courses.Add(currentCourse);
                        }

                        switch (key)
                        {
                            case "TenMonHoc":
                                currentCourse.TenMonHoc = value;
                                break;
                            case "MaMonHoc":
                                currentCourse.MaMonHoc = value;
                                break;
                            case "GiaoVien":
                                currentCourse.GiaoVien = value;
                                break;
                            case "NgayBatDau":
                                if (DateTime.TryParse(value, out DateTime ngayBatDau))
                                {
                                    currentCourse.NgayBatDau = ngayBatDau;
                                }
                                else
                                {
                                    Console.WriteLine($"Lỗi: Không thể chuyển đổi ngày {value}.");
                                }
                                currentCourse = null; // Kết thúc thông tin của sinh viên, sẵn sàng cho sinh viên tiếp theo
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi: " + e.Message);
        }
        Console.WriteLine("** Dựa trên tên môn học or giáo viên or mã môn học:");
        Console.Write("Nhập từ khoá tìm kiếm:");
        string keyword = Console.ReadLine();
        List<Courses> KetQuaTimKiem = TimKiemMonHoc(courses, keyword);

        // Hiển thị kết quả tìm kiếm
        Console.WriteLine("Kết quả tìm kiếm:");
        if (KetQuaTimKiem.Count != 0)
        {
            int itemsPerPage = 10; // Số lượng sinh viên hiển thị trên mỗi trang
            int currentPage = 0; // Trang hiện tại

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\tDanh sách tìm kiếm (Trang {0}):", currentPage + 1);
                Console.WriteLine();
                Console.WriteLine("{0,-40} {1,-25} {2,-30} {3,-15}", "Tên môn học", "Mã môn học", "Giáo viên", "Ngày bắt đầu");
                Console.WriteLine("==============================================================================================================================================");
                for (int i = currentPage * itemsPerPage; i < (currentPage + 1) * itemsPerPage && i < KetQuaTimKiem.Count; i++)
                {
                    var course = KetQuaTimKiem[i];
                    Console.WriteLine("{0,-40} {1,-25} {2,-30} {3,-15}", course.TenMonHoc, course.MaMonHoc, course.GiaoVien, course.NgayBatDau.ToString("dd/MM/yyyy"));
                    Console.WriteLine("==============================================================================================================================================");
                }

                Console.WriteLine("Ấn 'N' để xem trang tiếp theo, 'P' để xem trang trước, hoặc 'Q' để thoát.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.N && (currentPage + 1) * itemsPerPage < KetQuaTimKiem.Count)
                {
                    currentPage++;
                }
                else if (keyInfo.Key == ConsoleKey.P && currentPage > 0)
                {
                    currentPage--;
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine("\t \t Không tìm thấy môn học");
        }
        Console.WriteLine("=========================================");
        Console.WriteLine("\t1. Tìm Kiếm Tiếp");
        Console.WriteLine("\t2. Quay Lại Trang Chính");
        Console.WriteLine("\t3. Thoát");
        Console.Write("Vui lòng chọn chức năng thực hiện: ");
        while (true)
        {
            string choice2 = Console.ReadLine();
            if (choice2 == "1" || choice2 == "2" || choice2 == "3")
                switch (choice2)
                {
                    case "1":
                        TimKiemSinhVien();
                        break;
                    case "2":
                        TrangChinh();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            else Console.WriteLine("Chức năng không tồn tại, vui lòng chọn lại chức năng thực hiện: ");
        }
    }
    public static void ThemMonHoc()
    {
        Console.Clear();
        string ThemMonHocfilePath = "subjects.txt";
        List<Courses> courses = new List<Courses>();
        try
        {
            using (StreamReader sr = new StreamReader(ThemMonHocfilePath))
            {
                Courses currentCourse = null;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        if (currentCourse == null)
                        {
                            currentCourse = new Courses();
                            courses.Add(currentCourse);
                        }

                        switch (key)
                        {
                            case "TenMonHoc":
                                currentCourse.TenMonHoc = value;
                                break;
                            case "MaMonHoc":
                                currentCourse.MaMonHoc = value;
                                break;
                            case "GiaoVien":
                                currentCourse.GiaoVien = value;
                                break;
                            case "NgayBatDau":
                                if (DateTime.TryParse(value, out DateTime ngayBatDau))
                                {
                                    currentCourse.NgayBatDau = ngayBatDau;
                                }
                                else
                                {
                                    Console.WriteLine($"Lỗi: Không thể chuyển đổi ngày {value}.");
                                }
                                currentCourse = null; // Kết thúc thông tin của môn học, sẵn sàng cho môn học tiếp theo
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi: " + e.Message);
        }
        // Nhập thông tin mới cho môn học
        Console.Clear();
        Console.WriteLine("Nhập thông tin môn học:");
        Console.WriteLine("**Vui Lòng Nhập đúng định dạng");
        Console.Write("Tên môn học: ");
        string tenMonHoc = Console.ReadLine();
        Console.Write("Mã môn học: ");
        string maMonHoc = Console.ReadLine();
        Console.Write("Tên giáo viên: ");
        string giaoVien = Console.ReadLine();
        Console.Write("Ngày bắt đầu (yyyy-MM-dd): ");
        DateTime ngayBatDauHoc;
        while (true)
        {
            if (DateTime.TryParse(Console.ReadLine(), out ngayBatDauHoc))
                break;
            else Console.WriteLine("Ngày bắt đầu học không hợp lệ, vui lòng nhập lại");
        }
        string ngayBatDauInput = ngayBatDauHoc.ToString("yyyy/MM/dd");

        // Kiểm tra xem dữ liệu đã tồn tại trong danh sách hiện tại chưa bằng cách sử dụng hàm TimKiemMonHoc
        List<Courses> existingCourses = TimKiemMonHoc(courses, maMonHoc);

        if (existingCourses.Count != 0 || maMonHoc == null)
        {
            Console.WriteLine("Mã môn học không được trùng hoặc để trống");
            Console.ReadKey();
            Console.WriteLine("=========================================");
            Console.WriteLine("\t1. Thêm môn học tiếp");
            Console.WriteLine("\t2. Quay Lại Trang Chính");
            Console.WriteLine("\t3. Thoát");
            Console.Write("Vui lòng chọn chức năng thực hiện: ");
            while (true)
            {
                string choice2 = Console.ReadLine();
                if (choice2 == "1" || choice2 == "2" || choice2 == "3")
                    switch (choice2)
                    {
                        case "1":
                            ThemMonHoc();
                            break;
                        case "2":
                            TrangChinh();
                            break;
                        case "3":
                            Environment.Exit(0);
                            break;
                    }
                else Console.WriteLine("Chức năng không tồn tại, vui lòng chọn lại chức năng thực hiện: ");
            }
        }
        else
        {
            // Lưu danh sách mới vào tệp văn bản
            try
            {
                using (StreamWriter sw = new StreamWriter(ThemMonHocfilePath, true, Encoding.UTF8))// Mở tệp để viết, và chế độ true để thêm vào cuối tệp
                {
                    sw.WriteLine("TenMonHoc: " + tenMonHoc);
                    sw.WriteLine("MaMonHoc: " + maMonHoc);
                    sw.WriteLine("GiaoVien: " + giaoVien);
                    sw.WriteLine("NgayBatDau: " + ngayBatDauInput);
                    sw.WriteLine();
                }

                Console.WriteLine("Thêm môn học thành công.");
                Console.WriteLine("=========================================");
                Console.WriteLine("\t1. Thêm môn học tiếp");
                Console.WriteLine("\t2. Xem danh sách môn học");
                Console.WriteLine("\t3. Quay Lại Trang Chính");
                Console.WriteLine("\t4. Thoát");
                Console.Write("Vui lòng chọn chức năng thực hiện: ");
                string choice2 = Console.ReadLine();
                switch (choice2)
                {
                    case "1":
                        ThemMonHoc();
                        break;
                    case "2":
                        HienThiDanhSachMonHoc();
                        break;
                    case "3":
                        TrangChinh();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Lỗi: " + e.Message);
            }
        }
    }
    public static void ThemSinhVien()
    {
        Console.Clear();
        string ThemSinhVienfilePath = "students.txt"; // Thay thế đường dẫn bằng đường dẫn thực tế của tệp văn bản
        List<Students> students = new List<Students>();

        try
        {
            using (StreamReader sr = new StreamReader(ThemSinhVienfilePath))
            {
                Students currentStudent = null;

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        if (currentStudent == null)
                        {
                            currentStudent = new Students();
                            students.Add(currentStudent);
                        }

                        switch (key)
                        {
                            case "MaSoSinhVien":
                                currentStudent.MaSoSinhVien = value;
                                break;
                            case "TenSinhVien":
                                currentStudent.TenSinhVien = value;
                                break;
                            case "Email":
                                currentStudent.Email = value;
                                break;
                            case "GPA":
                                if (double.TryParse(value, out double gpa))
                                {
                                    currentStudent.GPA = gpa;
                                }
                                break;
                            case "XepLoai":
                                currentStudent.XepLoai = value;
                                currentStudent = null; // Kết thúc thông tin của sinh viên, sẵn sàng cho sinh viên tiếp theo
                                break;
                        }
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi: " + e.Message);
        }
        // Nhập thông tin mới cho sinh vien
        Console.Clear();
        Console.WriteLine("Nhập thông tin sinh viên:");
        Console.WriteLine("**Vui Lòng Nhập đúng định dạng");
        Console.Write("Mã số sinh viên: ");
        string maSoSinhVien = Console.ReadLine();
        Console.Write("Tên sinh viên: ");
        string tenSinhVien = Console.ReadLine();
        Console.Write("Email: ");
        string email = Console.ReadLine();
        Console.Write("GPA: ");
        Double gPA;
        while (true)
        {
            if (Double.TryParse(Console.ReadLine(), out gPA) && gPA >=0 && gPA <=4)
                break;
            else Console.WriteLine("GPA không hợp lệ, vui lòng nhập lại");
        }
        gPA = Math.Round(gPA,1);
        string xepLoai;
        if (gPA >= 0 && gPA < 1)
            xepLoai = "Kém";
        else if (gPA >= 1 && gPA < 2)
            xepLoai = "Yếu";
        else if (gPA >= 2 && gPA < 2.5)
            xepLoai = "Trung Bình";
        else if (gPA >= 2.5 && gPA < 3.2)
            xepLoai = "Khá";
        else if (gPA >= 3.2 && gPA < 3.6)
            xepLoai = "Giỏi";
        else xepLoai = "Xuất sắc";

        // Kiểm tra xem dữ liệu đã tồn tại trong danh sách hiện tại chưa bằng cách sử dụng hàm TimKiemSinhVien
        List<Students> existingStudents = TimKiemSinhVien(students, maSoSinhVien);

        if (existingStudents.Count != 0 || maSoSinhVien == null)
        {
            Console.WriteLine("Mã số sinh viên không được trùng hoặc để trống");
            Console.ReadKey();
            Console.WriteLine("=========================================");
            Console.WriteLine("\t1. Thêm sinh viên tiếp");
            Console.WriteLine("\t2. Quay Lại Trang Chính");
            Console.WriteLine("\t3. Thoát");
            Console.Write("Vui lòng chọn chức năng thực hiện: ");
            while (true)
            {
                string choice2 = Console.ReadLine();
                if (choice2 == "1" || choice2 == "2" || choice2 == "3")
                    switch (choice2)
                    {
                        case "1":
                            ThemSinhVien();
                            break;
                        case "2":
                            TrangChinh();
                            break;
                        case "3":
                            Environment.Exit(0);
                            break;
                    }
                else Console.WriteLine("Chức năng không tồn tại, vui lòng chọn lại chức năng thực hiện: ");
            }
        }
        else
        {
            // Lưu danh sách mới vào tệp văn bản
            try
            {
                using (StreamWriter sw = new StreamWriter(ThemSinhVienfilePath, true, Encoding.UTF8))// Mở tệp để viết, và chế độ true để thêm vào cuối tệp
                {
                    sw.WriteLine("MaSoSinhVien: " + maSoSinhVien);
                    sw.WriteLine("TenSinhVien: " + tenSinhVien);
                    sw.WriteLine("Email: " + email);
                    sw.WriteLine("GPA: " + gPA);
                    sw.WriteLine("XepLoai: " + xepLoai);
                    sw.WriteLine();
                }

                Console.WriteLine("Thêm sinh viên thành công.");
                Console.WriteLine("=========================================");
                Console.WriteLine("\t1. Thêm sinh viên tiếp");
                Console.WriteLine("\t2. Xem danh sách sinh viên");
                Console.WriteLine("\t3. Quay Lại Trang Chính");
                Console.WriteLine("\t4. Thoát");
                Console.Write("Vui lòng chọn chức năng thực hiện: ");
                string choice2 = Console.ReadLine();
                switch (choice2)
                {
                    case "1":
                        ThemMonHoc();
                        break;
                    case "2":
                        HienThiDanhSachSinhVien();
                        break;
                    case "3":
                        TrangChinh();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Lỗi: " + e.Message);
            }
        }
    }
    static List<Students> TimKiemSinhVien(List<Students> students, string keyword)
    {
        keyword = keyword.ToLower(); // Đưa keyword về dạng viết thường để tìm kiếm không phân biệt chữ hoa/chữ thường
        List<Students> results = new List<Students>();

        foreach (var student in students)
        {
            // Kiểm tra xem từ khoá có xuất hiện trong thông tin của sinh viên hay không
            if (student.MaSoSinhVien.ToLower().Contains(keyword) ||
                student.TenSinhVien.ToLower().Contains(keyword) ||
                student.Email.ToLower().Contains(keyword) ||
                student.GPA.ToString().Contains(keyword) ||
                student.XepLoai.ToLower().Contains(keyword))
            {
                results.Add(student);
            }
        }

        return results;
    }

    static List<Courses> TimKiemMonHoc(List<Courses> courses, string keyword)
    {
        keyword = keyword.ToLower(); // Đưa keyword về dạng viết thường để tìm kiếm không phân biệt chữ hoa/chữ thường
        List<Courses> results = new List<Courses>();

        foreach (var course in courses)
        {
            // Kiểm tra xem từ khoá có xuất hiện trong thông tin của sinh viên hay không
            if (course.TenMonHoc.ToLower().Contains(keyword) ||
                course.GiaoVien.ToLower().Contains(keyword) ||
                course.MaMonHoc.ToLower().Contains(keyword) ||
                course.NgayBatDau.ToString().Contains(keyword))
            {
                results.Add(course);
            }
        }

        return results;
    }
    public static void DangKyMonHoc()
    {
        Console.Clear();
        string filePath = "subjects.txt"; // Thay thế đường dẫn bằng đường dẫn thực tế của tệp văn bản
        List<Courses> courses = new List<Courses>();
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                Courses currentCourse = null;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        string value = parts[1].Trim();

                        if (currentCourse == null)
                        {
                            currentCourse = new Courses();
                            courses.Add(currentCourse);
                        }

                        switch (key)
                        {
                            case "TenMonHoc":
                                currentCourse.TenMonHoc = value;
                                break;
                            case "MaMonHoc":
                                currentCourse.MaMonHoc = value;
                                break;
                            case "GiaoVien":
                                currentCourse.GiaoVien = value;
                                break;
                            case "NgayBatDau":
                                if (DateTime.TryParse(value, out DateTime ngayBatDau))
                                {
                                    currentCourse.NgayBatDau = ngayBatDau;
                                }
                                else
                                {
                                    Console.WriteLine($"Lỗi: Không thể chuyển đổi ngày {value}.");
                                }
                                currentCourse = null; // Kết thúc thông tin của sinh viên, sẵn sàng cho sinh viên tiếp theo
                                break;
                        }
                    }
                }
            }
            int itemsPerPage = 10; // Số lượng sinh viên hiển thị trên mỗi trang
            int currentPage = 0; // Trang hiện tại

            while (true)
            {
                Console.Clear();
                Console.WriteLine("\tDanh sách môn học (Trang {0}):", currentPage + 1);
                Console.WriteLine();
                Console.WriteLine("{0,-40} {1,-25} {2,-30} {3,-15}", "Tên môn học", "Mã môn học", "Giáo viên", "Ngày bắt đầu");
                Console.WriteLine("==============================================================================================================================================");

                for (int i = currentPage * itemsPerPage; i < (currentPage + 1) * itemsPerPage && i < courses.Count; i++)
                {
                    var course = courses[i];
                    Console.WriteLine("{0,-40} {1,-25} {2,-30} {3,-15}", course.TenMonHoc, course.MaMonHoc, course.GiaoVien, course.NgayBatDau.ToString("dd/MM/yyyy"));
                    Console.WriteLine("==============================================================================================================================================");
                }

                Console.WriteLine("Ấn 'N' để xem trang tiếp theo, 'P' để xem trang trước, hoặc 'Q' để thoát.");

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.N && (currentPage + 1) * itemsPerPage < courses.Count)
                {
                    currentPage++;
                }
                else if (keyInfo.Key == ConsoleKey.P && currentPage > 0)
                {
                    currentPage--;
                }
                else if (keyInfo.Key == ConsoleKey.Q)
                {
                    break;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi: " + e.Message);
        }
        Console.Clear();
        Console.WriteLine("Đăng ký môn học:");
        Console.Write("Nhập mã số sinh viên: ");
        string maSoSinhVien = Console.ReadLine();

        Console.Write("Nhập tên sinh viên: ");
        string tenSinhVien = Console.ReadLine();

        Console.Write("Danh sách môn học đăng ký (cách nhau bằng dấu phẩy): ");
        string danhSachDangKy = Console.ReadLine();

        // Lưu thông tin đăng ký vào tệp văn bản mới
        string dangKyFilePath = "danhsachdangky.txt"; // Thay thế đường dẫn bằng đường dẫn thực tế của tệp đăng ký
        try
        {
            using (StreamWriter sw = new StreamWriter(dangKyFilePath, true, Encoding.UTF8))// Mở tệp để viết, và chế độ true để thêm vào cuối tệp
            {
                sw.WriteLine("MaSoSinhVien: " + maSoSinhVien);
                sw.WriteLine("TenSinhVien: " + tenSinhVien);
                sw.WriteLine("DanhSachDangKy: " + danhSachDangKy);
                sw.WriteLine("===============================================");
            }
            // Hiển thị ra danh sách đăng ký đọc từ file  danhsachdangky.txt
            Console.Clear();
            Console.WriteLine("\tDanh sách đăng ký môn học:");
            Console.WriteLine();
            string[] danhSachDangKyLines = File.ReadAllLines(dangKyFilePath, Encoding.UTF8);
            foreach (var line in danhSachDangKyLines)
            {
                Console.WriteLine(line);
            }

            // Cho người dùng chọn các tùy chọn tiếp theo
            Console.WriteLine("\nChọn tùy chọn:");
            Console.WriteLine("1. Tiếp tục đăng ký môn học");
            Console.WriteLine("2. Quay về trang chính");
            Console.WriteLine("3. Thoát chương trình");
            while (true)
            {
                string choice = Console.ReadLine();
                if (choice == "1" || choice == "2" || choice == "3")
                {
                    switch (choice)
                    {
                        case "1":
                            // Gọi lại phương thức DangKyMonHoc để tiếp tục đăng ký
                            DangKyMonHoc();
                            break;
                        case "2":
                            TrangChinh();
                            break;
                        case "3":
                            // Thoát chương trình
                            Environment.Exit(0);
                            break;
                    }
                }
                else Console.WriteLine("Lựa chọn không hợp lệ, vui lòng nhập lại");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Lỗi khi lưu thông tin đăng ký: " + e.Message);
        }

    }

    //public void Inthongtin()
    //{
    //    Console.WriteLine($"Tên môn học: {TenMonHoc}");
    //    Console.WriteLine($"Mã môn học: {MaMonHoc}");
    //    Console.WriteLine($"Giáo viên: {GiaoVien}");
    //    Console.WriteLine($"Ngày bắt đầu học: {Ngayhoc}");
    //}
    //public void KetQuaHocTap()
    //{
    //    Console.WriteLine($"Diem qua trinh: {DiemQuaTrinh}");
    //    Console.WriteLine($"Diem ket thuc hoc phan: {DiemKTHP}");
    //    //Diem trung binh
    //    DiemTB = Math.Round((DiemQuaTrinh + DiemKTHP) * 0.5, 1);
    //    Console.WriteLine($"Diem trung binh: {DiemTB}");
    //    //Xep loai
    //    if (DiemTB < 5)
    //        Console.WriteLine("Xep loai: Yeu");
    //    else if (DiemTB >= 5 && DiemTB < 6.5)
    //        Console.WriteLine("Xep loai: Trung Binh");
    //    else if (DiemTB >= 6.5 && DiemTB < 8)
    //        Console.WriteLine("Xep loai: Kha");
    //    else if (DiemTB >= 8 && DiemTB < 9)
    //        Console.WriteLine("Xep loai: Gioi");
    //    else Console.WriteLine("Xep loai: Xuat sac");
    //}

}
