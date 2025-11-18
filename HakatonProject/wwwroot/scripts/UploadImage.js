document.addEventListener("DOMContentLoaded", () => {
    const imageUploadInput = document.getElementById("eventImage");
    const imagePreviewBox = document.getElementById("image-preview-box");
    const fileNameDisplay = document.getElementById("file-name-display");

    if (imageUploadInput && imagePreviewBox && fileNameDisplay) {
        imageUploadInput.addEventListener("change", function() {
            const file = this.files[0];

            if (file) {
                fileNameDisplay.textContent = file.name;
                const reader = new FileReader();
                reader.onload = (e) => {
                    imagePreviewBox.innerHTML = ""; 
                    const img = document.createElement("img");
                    img.src = e.target.result; 
                    imagePreviewBox.appendChild(img);
                    imagePreviewBox.style.border = "2px solid rgba(255, 255, 255, 0.1)";
                };
                reader.readAsDataURL(file);

            } else {
                fileNameDisplay.textContent = "Файл не выбран";
                imagePreviewBox.innerHTML = "";
                imagePreviewBox.style.border = "2px dashed #5A5D75";
            }
        });
    }
});