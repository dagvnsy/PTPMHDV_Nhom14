document.addEventListener("DOMContentLoaded", function () {

    const taiKhoan = JSON.parse(localStorage.getItem("taiKhoanDangNhap"));
    const menuUser = document.getElementById("menu-user");

    if (!menuUser) return;

    if (!taiKhoan) {
        menuUser.innerHTML = `
            <a href="/html/login.html">Đăng nhập</a> /
            <a href="/html/dangky.html">Đăng ký</a>
        `;
    } else {
        menuUser.innerHTML = `
            Xin chào, <strong>${taiKhoan.TenDangNhap}</strong>
            |
            <a href="trangchuhv.html" onclick="dangXuat()">Đăng xuất</a>
        `;
    }
});

function dangXuat() {
    localStorage.removeItem("taiKhoanDangNhap");
    window.location.href = "/dangnhap.html";
}
