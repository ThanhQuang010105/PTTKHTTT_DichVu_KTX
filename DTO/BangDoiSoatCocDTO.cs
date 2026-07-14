using System;
using System.Collections.Generic;

namespace HomeStayDorm.DTO
{
    /// <summary>
    /// Thông tin hợp đồng dùng cho màn hình Lập bảng đối soát cọc.
    /// </summary>
    public class HopDongDoiSoatDTO
    {
        public string   MaHopDong        { get; set; }
        public string   MaKhachHang      { get; set; }
        public string   TenKhachHang     { get; set; }
        public string   SoDienThoai      { get; set; }
        public string   CCCD             { get; set; }
        public string   TenPhong         { get; set; }
        public DateTime NgayBatDau       { get; set; }
        public DateTime NgayKetThuc      { get; set; }
        public decimal  GiaThue          { get; set; }
        public string   TrangThai        { get; set; }
        public decimal  TienCocGoc       { get; set; }
        public string   MaPhieuCoc       { get; set; }
        public int      SoThangDaO       { get; set; }
        public bool     HetHanHopDong    { get; set; }
    }

    /// <summary>
    /// Một dòng phí phát sinh / khấu trừ.
    /// </summary>
    public class PhiPhatSinhDTO
    {
        public string   MaPhiPS          { get; set; }
        public string   LoaiPhi          { get; set; }
        public string   MoTa             { get; set; }
        public decimal  SoTien           { get; set; }
        public DateTime NgayGhiNhan      { get; set; }
        public string   NhanVienGhiNhan  { get; set; }
    }

    /// <summary>
    /// Kết quả tính toán đối soát — BLL tính ra, UI chỉ hiển thị.
    /// </summary>
    public class KetQuaDoiSoatDTO
    {
        // ── Thông tin đầu vào ────────────────────────────────────
        public string   MaHopDong            { get; set; }
        public decimal  TienCocGoc           { get; set; }
        public bool     DaKyHopDong          { get; set; }
        public bool     HetHanHopDong        { get; set; }
        public int      SoThangDaO           { get; set; }

        // ── Kết quả tính toán ────────────────────────────────────
        public decimal  TyLeHoanCoc          { get; set; }   // %
        public string   LyDoTyLe            { get; set; }   // Diễn giải
        public decimal  SoTienCocDuocXetHoan { get; set; }   // Cọc * Tỷ lệ
        public decimal  TongPhiPhatSinh      { get; set; }   // Tổng phí phát sinh
        public decimal  TongNoTienThue       { get; set; }   // Nợ tiền thuê
        public decimal  TongKhauTru          { get; set; }   // Tổng khấu trừ
        public decimal  SoTienHoanLai        { get; set; }   // >= 0: hoàn lại
        public decimal  SoTienPhaiDongThem   { get; set; }   // > 0: khách nợ thêm

        // ── Thông tin phụ trợ ────────────────────────────────────
        public DateTime NgayTraPhong         { get; set; }
        public string   GhiChu              { get; set; }
        public List<PhiPhatSinhDTO> DanhSachPhiPhatSinh { get; set; } = new();
    }

    /// <summary>
    /// Dữ liệu lưu bảng đối soát vào CSDL.
    /// </summary>
    public class LuuDoiSoatDTO
    {
        public string   MaDS                 { get; set; }
        public string   MaHopDong            { get; set; }
        public DateTime NgayTraPhong         { get; set; }
        public bool     DaKyHopDong          { get; set; }
        public bool     HetHanHopDong        { get; set; }
        public decimal  TyLeHoanCoc          { get; set; }
        public decimal  SoTienCocDuocXetHoan { get; set; }
        public decimal  TongTienKhauTru      { get; set; }
        public decimal  SoTienHoanLai        { get; set; }
        public decimal  SoTienPhaiDongThem   { get; set; }
        public string   GhiChu              { get; set; }
    }
}
