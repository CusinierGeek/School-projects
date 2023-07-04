const deletePopup = document.querySelector("#deletePopup");
const deleteConfirmBtn = document.querySelector("#deleteConfirm");
const deleteCancelBtn = document.querySelector("#deleteCancel");
const addPopup = document.querySelector("#addPopup");
const cancelBtn = document.querySelector("#addCancel");
const addBtn = document.querySelector("#addConfirm");
const skuInput = document.querySelector(".sku");
const container = document.querySelector(".container");
const brandInput = document.querySelector(".brand");
const nameInput = document.querySelector(".name");
const priceInput = document.querySelector(".price");
const descriptionInput = document.querySelector(".description");
const image = document.querySelector("#addImage");
const tableBody = document.querySelector("tbody");
const imageInput = document.querySelector("#imageInput");
const errorText = document.querySelector(".error");
const deleteImg = document.querySelector("#deleteImg");

cancelBtn.addEventListener("click", () => {
    hidePopup(addPopup);
    clearInputFields();
});
const errorHandling = (error) => {
    console.log(error);
    let errorMessage = error.response.data.error.message || error.response.data.error;

    if (errorMessage.includes("duplicate key error")) {
        errorMessage = "Le SKU existe déjà";
    } else if (errorMessage.includes("Item validation failed")) {
        switch (true) {
            case errorMessage.includes("sku"):
                errorMessage = "Le SKU doit être renseigné";
                break;
            case errorMessage.includes("sale_price: Price required"):
                errorMessage = "Le prix doit être renseigné";
                break;
            case errorMessage.includes("sale_price: Cast to Number failed"):
                errorMessage = "Le prix doit être un nombre";
                break;
            case errorMessage.includes("brand"):
                errorMessage = "La marque doit être renseignée";
                break;
            case errorMessage.includes("name"):
                errorMessage = "Le nom doit être renseigné";
                break;
            case errorMessage.includes("description"):
                errorMessage = "La description doit être renseignée";
                break;
        }
    } else if (errorMessage.includes("File too large")) {
        errorMessage = "Le fichier est trop volumineux";
        imageInput.value = "";
    }

    errorText.innerHTML = errorMessage;
};

const deleteItem = async (item) => {
    try {
        showPopup(deletePopup);
        const promise = await axios.get(`/items/update/${item.sku}`);
        const newItem = promise.data;
        const sku = newItem.sku;
        const name = newItem.name;
        const imageUrl = newItem.image_url;
        const popupText = document.querySelector(".popup-text");
        popupText.innerHTML = `
          Vous êtes sur le point de supprimer l'article :
          <br>
          <strong>Nom :</strong> ${name}
          <br>
          <strong>SKU :</strong> ${sku}
          <img src="${imageUrl}" width="100" alt="${name} id="deleteImg">
        `;
        deleteConfirmBtn.addEventListener("click", async () => {
            try {
                await axios.delete(`/items/${sku}`);
                removeItemRow(sku);
                console.log("Item deleted");
            } catch (error) {
                console.log(error);
            }
            hidePopup(deletePopup);
        });

        deleteCancelBtn.addEventListener("click", () => {
            hidePopup(deletePopup);
        });
    } catch (error) {
        console.log(error);
    }
};

const showAddPopup = () => {
    clearInputFields();
    skuInput.removeAttribute("readonly");
    skuInput.classList.remove("notAllowed");
    addBtn.removeEventListener("click", editBtnClickHandler);
    addBtn.addEventListener("click", addItem);
    addBtn.innerHTML = "Ajouter";
    image.src = "../images/placeholder.jpg";
    showPopup(addPopup);
};

const addItem = async () => {
    const formData = new FormData();
    formData.append("image", imageInput.files[0]);

    try {
        const sku = skuInput.value;
        const brand = brandInput.value;
        const name = nameInput.value;
        const price = priceInput.value;
        const description = descriptionInput.value;

        const item = {
            sku: sku,
            sale_price: price,
            brand: brand,
            name: name,
            description: description,
            image_url: "",
        };

        await axios.post("/items", item);
        if (imageInput.files[0]) {
            const response = await axios.post("/items/upload", formData, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            });
            imageUrl = response.data.replace("/public", "");
            image.src = imageUrl;
        }
        item.image_url = imageUrl;
        await axios.post("/items/update/", item);
        createItemRow(item);
        hidePopup(addPopup);
        imageInput.value = "";
    } catch (error) {
        errorHandling(error);
    }
};

const addImage = () => {
    imageInput.addEventListener("change", () => {
        if (imageInput.files.length) {
            const reader = new FileReader();
            reader.addEventListener("load", () => {
                image.src = reader.result;
            });
            reader.readAsDataURL(imageInput.files[0]);
        }
    });
    imageInput.click();
};

async function editBtnClickHandler() {
    try {
        const formData = new FormData();
        formData.append("image", imageInput.files[0]);
        if (imageInput.files[0]) {
            const response = await axios.post("/items/upload", formData, {
                headers: {
                    "Content-Type": "multipart/form-data",
                },
            });

            imageUrl = response.data.replace("/public", "");
            image.src = imageUrl;
        } else {
            imageUrl = image.src;
        }
        const updatedItem = {
            sku: skuInput.value,
            sale_price: priceInput.value,
            brand: brandInput.value,
            name: nameInput.value,
            description: descriptionInput.value,
            image_url: imageUrl,
        };

        await axios.post("/items/update/", updatedItem);
        updateItemRow(updatedItem);
        hidePopup(addPopup);
        clearInputFields();
        imageInput.value = "";
    } catch (error) {
        errorHandling(error);
    }
}

const editItem = async (item) => {
    const promise = await axios.get(`/items/update/${item.sku}`);
    const newItem = promise.data;
    showPopup(addPopup);
    skuInput.setAttribute("readonly", true);
    skuInput.classList.add("notAllowed");
    skuInput.value = newItem.sku;
    brandInput.value = newItem.brand;
    nameInput.value = newItem.name;
    priceInput.value = newItem.sale_price;
    descriptionInput.value = newItem.description;
    image.src = newItem.image_url;
    addBtn.innerHTML = "Modifier";
    addBtn.removeEventListener("click", addItem);
    addBtn.addEventListener("click", editBtnClickHandler);
};

const removeItemRow = (sku) => {
    const row = document.getElementById(sku);
    if (row) {
        tableBody.removeChild(row);
    }
};

const createItemRow = (item) => {
    const { sku, brand, name, sale_price, image_url } = item;
    const row = tableBody.insertRow(1);
    row.id = sku;
    const imageCell = row.insertCell();
    const skuCell = row.insertCell();
    const brandCell = row.insertCell();
    const nameCell = row.insertCell();
    const priceCell = row.insertCell();
    const actionsCell = row.insertCell();

    imageCell.innerHTML = `<img src="${image_url}" width="50px" alt="${name}">`;
    skuCell.textContent = sku;
    brandCell.textContent = brand;
    nameCell.textContent = name;
    priceCell.textContent = sale_price;

    actionsCell.innerHTML = `
        <i class="fas fa-edit" id="edit" onclick='editItem(${JSON.stringify(item)})'></i>
        <i class="fas fa-trash-alt" id="delete" onclick='deleteItem(${JSON.stringify(item)})'></i>
        <a href="/items/sku/${sku}">
            <i class="fas fa-info" id="info"></i>
        </a>
    `;
};

const updateItemRow = (item) => {
    const { sku, brand, name, sale_price, image_url } = item;
    const row = document.getElementById(sku);
    if (row) {
        const cells = row.cells;
        cells[0].innerHTML = `<img src="${image_url}" width="50px" alt="${name}">`;
        cells[1].textContent = sku;
        cells[2].textContent = brand;
        cells[3].textContent = name;
        cells[4].textContent = sale_price;
    }
};

const clearInputFields = () => {
    skuInput.value = "";
    brandInput.value = "";
    nameInput.value = "";
    priceInput.value = "";
    descriptionInput.value = "";
    image.src = "";
};

const showPopup = (popup) => {
    popup.classList.add("show");
    container.classList.add("overlay");
};

const hidePopup = (popup) => {
    popup.classList.remove("show");
    container.classList.remove("overlay");
    errorText.innerHTML = "";
};

const imageBtn = document.querySelector("#addImage");
imageBtn.addEventListener("click", addImage);
addBtn.addEventListener("click", addItem);
