using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSpeed
{
    // =========================================================
    // ABSTRACT CLASS PHUONG TIEN
    // =========================================================
    public abstract class PhuongTien
    {
        private string _maPT;
        private string _tenHang;
        private int _namSanXuat;
        private decimal _giaGoc;

        // MaPT
        public string MaPT
        {
            get { return _maPT; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _maPT = "PT000";
                else
                    _maPT = value;
            }
        }

        // TenHang
        public string TenHang
        {
            get { return _tenHang; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(
                        "Ten hang khong duoc de trong!");

                _tenHang = value;
            }
        }

        // NamSanXuat
        public int NamSanXuat
        {
            get { return _namSanXuat; }
            set
            {
                if (value < 1900 || value > DateTime.Now.Year)
                    throw new ArgumentException(
                        "Năm sản xuất không hợp lệ!");

                _namSanXuat = value;
            }
        }

        // GiaGoc
        public decimal GiaGoc
        {
            get { return _giaGoc; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(
                        "Gia goc phai lon hon 0!");

                _giaGoc = value;
            }
        }

        // Constructor
        public PhuongTien(
            string maPT,
            string tenHang,
            int namSanXuat,
            decimal giaGoc)
        {
            MaPT = maPT;
            TenHang = tenHang;
            NamSanXuat = namSanXuat;
            GiaGoc = giaGoc;
        }

        // Abstract
        public abstract decimal TinhGiaLanBanh();

        // Virtual
        public virtual string GetInfo()
        {
            return $"MaPT: {MaPT} | Hang: {TenHang} | " +
                   $"Nam SX: {NamSanXuat} | " +
                   $"Gia goc: {GiaGoc:N0}";
        }
    }


    // =========================================================
    // CLASS O TO
    // =========================================================
    public class OTo : PhuongTien
    {
        private int _soChoNgoi;
        private double _dungTichDongCo;

        // SoChoNgoi
        public int SoChoNgoi
        {
            get { return _soChoNgoi; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(
                        "So cho ngoi phai lon hon 0!");

                _soChoNgoi = value;
            }
        }

        // DungTichDongCo
        public double DungTichDongCo
        {
            get { return _dungTichDongCo; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(
                        "Dung tich dong co phai lon hon 0!");

                _dungTichDongCo = value;
            }
        }

        // Constructor
        public OTo(
            string maPT,
            string tenHang,
            int namSanXuat,
            decimal giaGoc,
            int soChoNgoi,
            double dungTichDongCo)
            : base(
                maPT,
                tenHang,
                namSanXuat,
                giaGoc)
        {
            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        // Tinh gia lan banh
        public override decimal TinhGiaLanBanh()
        {
            // <= 9 cho: +12% +30%
            if (SoChoNgoi <= 9)
            {
                return GiaGoc
                       + GiaGoc * 0.12m
                       + GiaGoc * 0.30m;
            }

            // > 9 cho: +10%
            return GiaGoc
                   + GiaGoc * 0.10m;
        }

        // Override GetInfo
        public override string GetInfo()
        {
            return base.GetInfo()
                   + $" | So cho: {SoChoNgoi}"
                   + $" | Dong co: {DungTichDongCo}L";
        }
    }


    // =========================================================
    // CLASS XE MAY
    // =========================================================
    public class XeMay : PhuongTien
    {
        private double _dungTichXylanh;

        // DungTichXylanh
        public double DungTichXylanh
        {
            get { return _dungTichXylanh; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException(
                        "Dung tich xylanh phai lon hon 0!");

                _dungTichXylanh = value;
            }
        }

        // Constructor
        public XeMay(
            string maPT,
            string tenHang,
            int namSanXuat,
            decimal giaGoc,
            double dungTichXylanh)
            : base(
                maPT,
                tenHang,
                namSanXuat,
                giaGoc)
        {
            DungTichXylanh = dungTichXylanh;
        }

        // Tinh gia lan banh
        public override decimal TinhGiaLanBanh()
        {
            // < 175cc: +2%
            if (DungTichXylanh < 175)
            {
                return GiaGoc
                       + GiaGoc * 0.02m;
            }

            // >= 175cc: +5%
            return GiaGoc
                   + GiaGoc * 0.05m;
        }

        // Override GetInfo
        public override string GetInfo()
        {
            return base.GetInfo()
                   + $" | Xylanh: {DungTichXylanh}cc";
        }
    }


    // =========================================================
    // QUAN LY PHUONG TIEN
    // =========================================================
    public class QuanLyPhuongTien
    {
        private List<PhuongTien> danhSach;

        public QuanLyPhuongTien()
        {
            danhSach = new List<PhuongTien>();
        }

        // Them phuong tien
        public void AddPhuongTien(PhuongTien pt)
        {
            danhSach.Add(pt);
        }

        // Lay danh sach
        public List<PhuongTien> GetDanhSach()
        {
            return danhSach;
        }

        // Hien thi tat ca
        public void DisplayAll()
        {
            if (danhSach.Count == 0)
            {
                Console.WriteLine(
                    "Danh sach phuong tien dang rong!");
                return;
            }

            foreach (PhuongTien pt in danhSach)
            {
                Console.WriteLine(pt.GetInfo());

                Console.WriteLine(
                    $"Gia lan banh: " +
                    $"{pt.TinhGiaLanBanh():N0} VNĐ");

                Console.WriteLine(
                    "------------------------------------------------------------");
            }
        }

        // Tim gia lan banh lon nhat
        public PhuongTien FindMaxGiaLanBanh()
        {
            if (danhSach.Count == 0)
                return null;

            return danhSach
                .OrderByDescending(
                    x => x.TinhGiaLanBanh())
                .First();
        }

        // Tim theo ten hang
        public List<PhuongTien> SearchByName(
            string tenHang)
        {
            return danhSach
                .Where(x =>
                    x.TenHang.Contains(
                        tenHang,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }


    // =========================================================
    // PROGRAM
    // =========================================================
    class Program
    {
        // Quan ly danh sach chinh
        static QuanLyPhuongTien ql =
            new QuanLyPhuongTien();


        // =====================================================
        // KHOI TAO DU LIEU MAU
        // =====================================================
        static void KhoiTaoDuLieu()
        {
            // ---------------- O TO ----------------

            ql.AddPhuongTien(
                new OTo(
                    "OT001",
                    "Toyota",
                    2025,
                    1000000000m,
                    5,
                    1.5));

            ql.AddPhuongTien(
                new OTo(
                    "OT002",
                    "Honda",
                    2024,
                    800000000m,
                    7,
                    2.0));

            ql.AddPhuongTien(
                new OTo(
                    "OT003",
                    "Ford",
                    2023,
                    1500000000m,
                    12,
                    2.5));


            // ---------------- XE MAY ----------------

            ql.AddPhuongTien(
                new XeMay(
                    "XM001",
                    "Honda",
                    2025,
                    50000000m,
                    150));

            ql.AddPhuongTien(
                new XeMay(
                    "XM002",
                    "Yamaha",
                    2024,
                    45000000m,
                    125));

            ql.AddPhuongTien(
                new XeMay(
                    "XM003",
                    "Suzuki",
                    2023,
                    60000000m,
                    175));
        }


        // =====================================================
        // HIEN THI O TO
        // =====================================================
        static void HienThiOTo()
        {
            Console.Clear();

            Console.WriteLine(
                "============================================================");

            Console.WriteLine(
                "                     DANH SACH O TO");

            Console.WriteLine(
                "============================================================");

            List<PhuongTien> danhSachOTo =
                ql.GetDanhSach()
                  .Where(x => x is OTo)
                  .ToList();

            foreach (PhuongTien pt in danhSachOTo)
            {
                Console.WriteLine(pt.GetInfo());

                Console.WriteLine(
                    $"Gia lan banh: " +
                    $"{pt.TinhGiaLanBanh():N0} VNĐ");

                Console.WriteLine(
                    "------------------------------------------------------------");
            }

            DungManHinh();
        }


        // =====================================================
        // HIEN THI XE MAY
        // =====================================================
        static void HienThiXeMay()
        {
            Console.Clear();

            Console.WriteLine(
                "============================================================");

            Console.WriteLine(
                "                   DANH SACH XE MAY");

            Console.WriteLine(
                "============================================================");

            List<PhuongTien> danhSachXeMay =
                ql.GetDanhSach()
                  .Where(x => x is XeMay)
                  .ToList();

            foreach (PhuongTien pt in danhSachXeMay)
            {
                Console.WriteLine(pt.GetInfo());

                Console.WriteLine(
                    $"Gia lan banh: " +
                    $"{pt.TinhGiaLanBanh():N0} VNĐ");

                Console.WriteLine(
                    "------------------------------------------------------------");
            }

            DungManHinh();
        }


        // =====================================================
        // HIEN THI TAT CA
        // =====================================================
        static void HienThiTatCa()
        {
            Console.Clear();

            Console.WriteLine(
                "============================================================");

            Console.WriteLine(
                "                  TAT CA PHUONG TIEN");

            Console.WriteLine(
                "============================================================");

            ql.DisplayAll();

            DungManHinh();
        }


        // =====================================================
        // TIM GIA LAN BANH CAO NHAT
        // =====================================================
        static void TimMax()
        {
            Console.Clear();

            Console.WriteLine(
                "============================================================");

            Console.WriteLine(
                "              PHUONG TIEN CO GIA LAN BANH CAO NHAT");

            Console.WriteLine(
                "============================================================");

            PhuongTien pt =
                ql.FindMaxGiaLanBanh();

            if (pt == null)
            {
                Console.WriteLine(
                    "Danh sach dang rong!");
            }
            else
            {
                Console.WriteLine(pt.GetInfo());

                Console.WriteLine(
                    $"Gia lan banh cao nhat: " +
                    $"{pt.TinhGiaLanBanh():N0} VNĐ");
            }

            DungManHinh();
        }


        // =====================================================
        // TIM KIEM THEO TEN HANG
        // =====================================================
        static void TimKiemTheoTen()
        {
            Console.Clear();

            Console.WriteLine(
                "============================================================");

            Console.WriteLine(
                "                 TIM KIEM THEO TEN HANG");

            Console.WriteLine(
                "============================================================");

            Console.Write(
                "Nhap ten hang can tim: ");

            string tenHang =
                Console.ReadLine();

            List<PhuongTien> ketQua =
                ql.SearchByName(tenHang);

            if (ketQua.Count == 0)
            {
                Console.WriteLine(
                    "Khong tim thay phuong tien!");
            }
            else
            {
                Console.WriteLine(
                    $"\nTim thay {ketQua.Count} phuong tien:");

                foreach (PhuongTien pt in ketQua)
                {
                    Console.WriteLine(
                        pt.GetInfo());

                    Console.WriteLine(
                        $"Gia lan banh: " +
                        $"{pt.TinhGiaLanBanh():N0} VNĐ");

                    Console.WriteLine(
                        "------------------------------------------------------------");
                }
            }

            DungManHinh();
        }


        // =====================================================
        // TINH GIA THEO CONG THUC
        // =====================================================
        static decimal TinhGiaTheoCongThuc(
            PhuongTien pt)
        {
            if (pt is OTo oTo)
            {
                if (oTo.SoChoNgoi <= 9)
                {
                    return oTo.GiaGoc
                           + oTo.GiaGoc * 0.12m
                           + oTo.GiaGoc * 0.30m;
                }

                return oTo.GiaGoc
                       + oTo.GiaGoc * 0.10m;
            }

            if (pt is XeMay xeMay)
            {
                if (xeMay.DungTichXylanh < 175)
                {
                    return xeMay.GiaGoc
                           + xeMay.GiaGoc * 0.02m;
                }

                return xeMay.GiaGoc
                       + xeMay.GiaGoc * 0.05m;
            }

            return 0;
        }


        // =====================================================
        // TC01
        // VALIDATION NAM SAN XUAT
        // =====================================================
        static bool TC01()
        {
            Console.WriteLine(
                "\nTC01 - KIEM TRA VALIDATION NAM SAN XUAT");

            Console.Write(
                "Nhap nam san xuat: ");

            int nam;

            while (!int.TryParse(
                Console.ReadLine(),
                out nam))
            {
                Console.Write(
                    "Nhap lai nam san xuat: ");
            }

            try
            {
                OTo test = new OTo(
                    "TC01",
                    "Test OTo",
                    nam,
                    1000000000m,
                    5,
                    1.5);

                Console.WriteLine(
                    "FAIL - Khong phat sinh ArgumentException!");

                return false;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(
                    $"Bat duoc loi: {ex.Message}");

                bool pass =
                    ex.Message ==
                    "Năm sản xuất không hợp lệ!";

                Console.WriteLine(
                    pass
                    ? "PASS"
                    : "FAIL");

                return pass;
            }
        }


        // =====================================================
        // TC02
        // TINH GIA LAN BANH O TO
        // =====================================================
        static bool TC02()
        {
            Console.WriteLine(
                "\nTC02 - KIEM TRA TINH GIA LAN BANH O TO");

            decimal giaGoc =
                NhapDecimal(
                    "Nhap gia goc O To: ");

            int soCho =
                NhapInt(
                    "Nhap so cho O To: ");

            OTo oTo = new OTo(
                "TC02",
                "Test OTo",
                DateTime.Now.Year,
                giaGoc,
                soCho,
                1.5);

            decimal expected =
                TinhGiaTheoCongThuc(oTo);

            decimal actual =
                oTo.TinhGiaLanBanh();

            Console.WriteLine(
                "\n--- KET QUA TC02 ---");

            Console.WriteLine(
                $"Gia goc: {giaGoc:N0} VNĐ");

            Console.WriteLine(
                $"So cho: {soCho}");

            if (soCho <= 9)
            {
                Console.WriteLine(
                    "Cong thuc: Gia goc + 12% + 30%");
            }
            else
            {
                Console.WriteLine(
                    "Cong thuc: Gia goc + 10%");
            }

            Console.WriteLine(
                $"Expected: {expected:N0} VNĐ");

            Console.WriteLine(
                $"Actual:   {actual:N0} VNĐ");

            bool pass =
                actual == expected;

            Console.WriteLine(
                pass ? "PASS" : "FAIL");

            return pass;
        }


        // =====================================================
        // TC03
        // TINH GIA LAN BANH XE MAY
        // =====================================================
        static bool TC03()
        {
            Console.WriteLine(
                "\nTC03 - KIEM TRA TINH GIA LAN BANH XE MAY");

            decimal giaGoc =
                NhapDecimal(
                    "Nhap gia goc Xe May: ");

            double xylanh =
                NhapDouble(
                    "Nhap dung tich xylanh: ");

            XeMay xeMay = new XeMay(
                "TC03",
                "Test Xe May",
                DateTime.Now.Year,
                giaGoc,
                xylanh);

            decimal expected =
                TinhGiaTheoCongThuc(xeMay);

            decimal actual =
                xeMay.TinhGiaLanBanh();

            Console.WriteLine(
                "\n--- KET QUA TC03 ---");

            Console.WriteLine(
                $"Gia goc: {giaGoc:N0} VNĐ");

            Console.WriteLine(
                $"Xylanh: {xylanh}cc");

            if (xylanh < 175)
            {
                Console.WriteLine(
                    "Cong thuc: Gia goc + 2%");
            }
            else
            {
                Console.WriteLine(
                    "Cong thuc: Gia goc + 5%");
            }

            Console.WriteLine(
                $"Expected: {expected:N0} VNĐ");

            Console.WriteLine(
                $"Actual:   {actual:N0} VNĐ");

            bool pass =
                actual == expected;

            Console.WriteLine(
                pass ? "PASS" : "FAIL");

            return pass;
        }


        // =====================================================
        // TC04
        // DA HINH LIST<PHUONGTIEN>
        // =====================================================
        static bool TC04()
        {
            Console.WriteLine(
                "\nTC04 - KIEM TRA DA HINH");

            List<PhuongTien> danhSach =
                new List<PhuongTien>();


            // ---------------- O TO ----------------

            Console.WriteLine(
                "\n--- NHAP O TO ---");

            decimal giaOTo =
                NhapDecimal(
                    "Nhap gia goc O To: ");

            int soCho =
                NhapInt(
                    "Nhap so cho O To: ");

            OTo oTo = new OTo(
                "TC04-OT",
                "Test OTo",
                DateTime.Now.Year,
                giaOTo,
                soCho,
                1.5);

            danhSach.Add(oTo);


            // ---------------- XE MAY ----------------

            Console.WriteLine(
                "\n--- NHAP XE MAY ---");

            decimal giaXeMay =
                NhapDecimal(
                    "Nhap gia goc Xe May: ");

            double xylanh =
                NhapDouble(
                    "Nhap dung tich xylanh: ");

            XeMay xeMay = new XeMay(
                "TC04-XM",
                "Test Xe May",
                DateTime.Now.Year,
                giaXeMay,
                xylanh);

            danhSach.Add(xeMay);


            // ---------------- KET QUA ----------------

            Console.WriteLine(
                "\n============================================================");

            Console.WriteLine(
                "                  KET QUA TC04 - DA HINH");

            Console.WriteLine(
                "============================================================");

            bool pass = true;


            // O TO
            Console.WriteLine(
                "\n[1] O TO");

            Console.WriteLine(
                $"Gia goc: {oTo.GiaGoc:N0} VNĐ");

            Console.WriteLine(
                $"So cho ngoi: {oTo.SoChoNgoi}");

            decimal expectedOTo =
                TinhGiaTheoCongThuc(oTo);

            decimal actualOTo =
                oTo.TinhGiaLanBanh();

            Console.WriteLine(
                $"Expected: {expectedOTo:N0} VNĐ");

            Console.WriteLine(
                $"Actual:   {actualOTo:N0} VNĐ");

            if (expectedOTo != actualOTo)
                pass = false;


            // XE MAY
            Console.WriteLine(
                "\n[2] XE MAY");

            Console.WriteLine(
                $"Gia goc: {xeMay.GiaGoc:N0} VNĐ");

            Console.WriteLine(
                $"Xylanh: {xeMay.DungTichXylanh}cc");

            decimal expectedXeMay =
                TinhGiaTheoCongThuc(xeMay);

            decimal actualXeMay =
                xeMay.TinhGiaLanBanh();

            Console.WriteLine(
                $"Expected: {expectedXeMay:N0} VNĐ");

            Console.WriteLine(
                $"Actual:   {actualXeMay:N0} VNĐ");

            if (expectedXeMay != actualXeMay)
                pass = false;


            // CHUNG MINH DA HINH
            Console.WriteLine(
                "\n--- KIEM TRA DA HINH ---");

            Console.WriteLine(
                "List<PhuongTien> dang chua:");

            foreach (PhuongTien pt in danhSach)
            {
                Console.WriteLine(
                    $"- {pt.GetType().Name}");

                Console.WriteLine(
                    $"  Gia lan banh: " +
                    $"{pt.TinhGiaLanBanh():N0} VNĐ");
            }

            Console.WriteLine(
                "\nC# tu dong goi dung TinhGiaLanBanh()");

            Console.WriteLine(
                "cua tung lop OTo va XeMay.");

            Console.WriteLine(
                pass
                ? "\nTC04: PASS"
                : "\nTC04: FAIL");

            return pass;
        }


        // =====================================================
        // TC05
        // TIM GIA LAN BANH MAX
        // =====================================================
        static bool TC05()
        {
            Console.WriteLine(
                "\nTC05 - KIEM TRA TIM GIA LAN BANH MAX");

            QuanLyPhuongTien testQL =
                new QuanLyPhuongTien();


            // ---------------- PHUONG TIEN 1 ----------------

            Console.WriteLine(
                "\n--- NHAP PHUONG TIEN 1 ---");

            decimal giaOTo1 =
                NhapDecimal(
                    "Nhap gia goc O To 1: ");

            int soCho1 =
                NhapInt(
                    "Nhap so cho O To 1: ");

            OTo oTo1 = new OTo(
                "TC05-OT1",
                "Toyota",
                DateTime.Now.Year,
                giaOTo1,
                soCho1,
                1.5);

            testQL.AddPhuongTien(oTo1);


            // ---------------- PHUONG TIEN 2 ----------------

            Console.WriteLine(
                "\n--- NHAP PHUONG TIEN 2 ---");

            decimal giaOTo2 =
                NhapDecimal(
                    "Nhap gia goc O To 2: ");

            int soCho2 =
                NhapInt(
                    "Nhap so cho O To 2: ");

            OTo oTo2 = new OTo(
                "TC05-OT2",
                "Honda",
                DateTime.Now.Year,
                giaOTo2,
                soCho2,
                2.0);

            testQL.AddPhuongTien(oTo2);


            // ---------------- PHUONG TIEN 3 ----------------

            Console.WriteLine(
                "\n--- NHAP PHUONG TIEN 3 ---");

            decimal giaXeMay =
                NhapDecimal(
                    "Nhap gia goc Xe May: ");

            double xylanh =
                NhapDouble(
                    "Nhap dung tich xylanh: ");

            XeMay xeMay = new XeMay(
                "TC05-XM1",
                "Honda",
                DateTime.Now.Year,
                giaXeMay,
                xylanh);

            testQL.AddPhuongTien(xeMay);


            // ---------------- HIEN THI ----------------

            Console.WriteLine(
                "\n============================================================");

            Console.WriteLine(
                "                 DANH SACH TEST TC05");

            Console.WriteLine(
                "============================================================");

            foreach (PhuongTien pt
                     in testQL.GetDanhSach())
            {
                Console.WriteLine(
                    pt.GetInfo());

                Console.WriteLine(
                    $"Gia lan banh: " +
                    $"{pt.TinhGiaLanBanh():N0} VNĐ");

                Console.WriteLine(
                    "------------------------------------------------------------");
            }


            // ---------------- FIND MAX ----------------

            PhuongTien ketQua =
                testQL.FindMaxGiaLanBanh();

            Console.WriteLine(
                "\n--- KET QUA FINDMAXGIALANBANH() ---");

            if (ketQua == null)
            {
                Console.WriteLine(
                    "FAIL - Khong tim thay phuong tien!");

                return false;
            }

            Console.WriteLine(
                "Phuong tien co gia lan banh cao nhat:");

            Console.WriteLine(
                ketQua.GetInfo());

            Console.WriteLine(
                $"Gia lan banh: " +
                $"{ketQua.TinhGiaLanBanh():N0} VNĐ");


            // ---------------- DOI CHIEU ----------------

            decimal expected =
                1420000000m;

            bool dungLoai =
                ketQua is OTo;

            bool dungSoCho =
                dungLoai &&
                ((OTo)ketQua).SoChoNgoi == 5;

            bool dungGia =
                ketQua.TinhGiaLanBanh()
                == expected;


            Console.WriteLine(
                "\n--- DOI CHIEU KET QUA ---");

            Console.WriteLine(
                $"Expected: O To 5 cho - " +
                $"{expected:N0} VNĐ");

            Console.WriteLine(
                $"Dung loai O To: " +
                $"{(dungLoai ? "YES" : "NO")}");

            Console.WriteLine(
                $"Dung 5 cho: " +
                $"{(dungSoCho ? "YES" : "NO")}");

            Console.WriteLine(
                $"Dung gia 1.42 ty: " +
                $"{(dungGia ? "YES" : "NO")}");


            bool pass =
                dungLoai &&
                dungSoCho &&
                dungGia;

            Console.WriteLine(
                pass
                ? "\nTC05: PASS"
                : "\nTC05: FAIL");

            return pass;
        }


        // =====================================================
        // CHAY TAT CA TEST CASE
        // =====================================================
        static void ChayTatCaTestCase()
        {
            Console.Clear();

            Console.WriteLine(
                "============================================================");

            Console.WriteLine(
                "                    AUTOSPEED - TEST CASE");

            Console.WriteLine(
                "============================================================");


            // TC01
            bool tc01 = TC01();

            Console.WriteLine(
                $"\nTC01: {(tc01 ? "PASS" : "FAIL")}");

            Console.WriteLine(
                "\n------------------------------------------------------------");


            // TC02
            bool tc02 = TC02();

            Console.WriteLine(
                $"\nTC02: {(tc02 ? "PASS" : "FAIL")}");

            Console.WriteLine(
                "\n------------------------------------------------------------");


            // TC03
            bool tc03 = TC03();

            Console.WriteLine(
                $"\nTC03: {(tc03 ? "PASS" : "FAIL")}");

            Console.WriteLine(
                "\n------------------------------------------------------------");


            // TC04
            bool tc04 = TC04();

            Console.WriteLine(
                $"\nTC04: {(tc04 ? "PASS" : "FAIL")}");

            Console.WriteLine(
                "\n------------------------------------------------------------");


            // TC05
            bool tc05 = TC05();

            Console.WriteLine(
                $"\nTC05: {(tc05 ? "PASS" : "FAIL")}");


            // ---------------- TONG KET ----------------

            Console.WriteLine(
                "\n============================================================");

            Console.WriteLine(
                "                    TONG KET TEST CASE");

            Console.WriteLine(
                "============================================================");

            Console.WriteLine(
                $"TC01: {(tc01 ? "PASS" : "FAIL")}");

            Console.WriteLine(
                $"TC02: {(tc02 ? "PASS" : "FAIL")}");

            Console.WriteLine(
                $"TC03: {(tc03 ? "PASS" : "FAIL")}");

            Console.WriteLine(
                $"TC04: {(tc04 ? "PASS" : "FAIL")}");

            Console.WriteLine(
                $"TC05: {(tc05 ? "PASS" : "FAIL")}");

            Console.WriteLine(
                "============================================================");

            DungManHinh();
        }


        // =====================================================
        // NHAP INT
        // =====================================================
        static int NhapInt(string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);

                string input =
                    Console.ReadLine();

                int value;

                if (
                    int.TryParse(
                        input,
                        out value)
                    && value > 0)
                {
                    return value;
                }

                Console.WriteLine(
                    "Vui long nhap so nguyen > 0!");
            }
        }


        // =====================================================
        // NHAP DECIMAL
        // =====================================================
        static decimal NhapDecimal(
            string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);

                string input =
                    Console.ReadLine();

                decimal value;

                if (
                    decimal.TryParse(
                        input,
                        out value)
                    && value > 0)
                {
                    return value;
                }

                Console.WriteLine(
                    "Vui long nhap so > 0!");
            }
        }


        // =====================================================
        // NHAP DOUBLE
        // =====================================================
        static double NhapDouble(
            string thongBao)
        {
            while (true)
            {
                Console.Write(thongBao);

                string input =
                    Console.ReadLine();

                double value;

                if (
                    double.TryParse(
                        input,
                        out value)
                    && value > 0)
                {
                    return value;
                }

                Console.WriteLine(
                    "Vui long nhap so > 0!");
            }
        }


        // =====================================================
        // DUNG MAN HINH
        // =====================================================
        static void DungManHinh()
        {
            Console.WriteLine(
                "\nNhan Enter de quay lai menu...");

            Console.ReadLine();
        }


        // =====================================================
        // MENU
        // =====================================================
        static void Menu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine(
                    "========================================");

                Console.WriteLine(
                    "      QUAN LY PHUONG TIEN AUTOSPEED");

                Console.WriteLine(
                    "========================================");

                Console.WriteLine(
                    "1. Hien thi danh sach O To");

                Console.WriteLine(
                    "2. Hien thi danh sach Xe May");

                Console.WriteLine(
                    "3. Hien thi tat ca phuong tien");

                Console.WriteLine(
                    "4. Tim phuong tien gia lanh banh cao nhat");

                Console.WriteLine(
                    "5. Tim kiem theo ten hang");

                Console.WriteLine(
                    "6. Chay 5 Test Case");

                Console.WriteLine(
                    "0. Thoat");

                Console.WriteLine(
                    "========================================");

                Console.Write(
                    "Chon chuc nang: ");

                string luaChon =
                    Console.ReadLine();


                switch (luaChon)
                {
                    case "1":
                        HienThiOTo();
                        break;

                    case "2":
                        HienThiXeMay();
                        break;

                    case "3":
                        HienThiTatCa();
                        break;

                    case "4":
                        TimMax();
                        break;

                    case "5":
                        TimKiemTheoTen();
                        break;

                    case "6":
                        ChayTatCaTestCase();
                        break;

                    case "0":
                        Console.WriteLine(
                            "Da thoat chuong trinh.");
                        return;

                    default:
                        Console.WriteLine(
                            "Lua chon khong hop le!");

                        Console.WriteLine(
                            "Nhan Enter de tiep tuc...");

                        Console.ReadLine();
                        break;
                }
            }
        }


        // =====================================================
        // MAIN
        // =====================================================
        static void Main(string[] args)
        {
            // Nap du lieu mau cho menu
            KhoiTaoDuLieu();

            // Chay menu
            Menu();
        }
    }
}