// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.getElementById("btnAddCategory").addEventListener("click", function () {
    let newCategory = prompt("Nhập tên danh mục mới:");

    if (!newCategory || newCategory.trim() === "") {
        showToast("Tên danh mục không hợp lệ!", "error");
        return; // Dừng lại nếu danh mục không hợp lệ
    }

    let categorySelect = document.getElementById("categorySelect");
    let optionExists = [...categorySelect.options].some(opt => opt.text === newCategory);

    if (optionExists) {
        showToast("Danh mục đã tồn tại!", "warning");
        return; // Không thêm trùng lặp
    }

    // Tạo danh mục mới
    let newOption = document.createElement("option");
    newOption.text = newCategory;
    newOption.value = "new-" + new Date().getTime();
    categorySelect.appendChild(newOption);
    categorySelect.value = newOption.value;

    showToast("Thêm danh mục thành công!", "success");
    setTimeout(hideModal, 500); // Đợi 500ms rồi tắt popup để tránh lỗi
});

// Hàm ẩn modal (hỗ trợ cả Bootstrap Modal và HTML5 Dialog)
function hideModal() {
    let modal = document.querySelector('.modal.show');
    if (modal) {
        let modalInstance = bootstrap.Modal.getInstance(modal);
        modalInstance.hide();
    }

    let dialog = document.querySelector("dialog[open]");
    if (dialog) {
        dialog.close();
    }
}

// Xóa danh mục được chọn
document.getElementById("btnRemoveCategory").addEventListener("click", function () {
    let categorySelect = document.getElementById("categorySelect");
    let selectedOption = categorySelect.options[categorySelect.selectedIndex];

    if (!selectedOption || selectedOption.value === "") {
        showToast("Vui lòng chọn danh mục cần xóa!", "warning");
        return;
    }

    selectedOption.remove();
    showToast("Đã xóa danh mục!", "success");
});

// Hiển thị toast thay vì alert
function showToast(message, type = "info") {
    let toast = document.createElement("div");
    toast.className = `toast-message toast-${type}`;
    toast.innerText = message;
    document.body.appendChild(toast);

    setTimeout(() => {
        toast.remove();
    }, 2000);
}


function previewImage(event) {
    var input = event.target;
    var preview = document.getElementById("imagePreview");

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            preview.src = e.target.result; // Hiển thị ảnh được chọn
            preview.classList.remove("d-none"); // Hiển thị ảnh nếu đang ẩn
        };

        reader.readAsDataURL(input.files[0]);
    }
}
