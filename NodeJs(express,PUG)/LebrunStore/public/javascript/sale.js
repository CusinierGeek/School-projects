const finalasationButton = document.getElementById("finalisation");
const deleteButtons = document.querySelectorAll("#delete");
const updateButtons = document.querySelectorAll("#edit");

$(document).ready(function () {
    $("#mySelect").select2({
        placeholder: "Choisir un produit",
        width: "70%",
        allowClear: true,
    });
});

finalasationButton.addEventListener("click", async function () {
    try {
        const url = "/sale/finalisation";
        const data = { key: "value" };
        const response = await fetch(url, {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        });

        if (response.ok) {
            console.log("La requête POST a été envoyée avec succès.");
            window.location.href = "/sale";
        } else {
            console.error("Erreur lors de l'envoi de la requête POST.");
        }
    } catch (error) {
        console.error("Erreur lors de l'envoi de la requête POST:", error);
    }
});

deleteButtons.forEach((deleteButton) => {
    deleteButton.addEventListener("click", async function () {
        try {
            const sku = deleteButton.getAttribute("data-sku");
            const url = "/line/delete";
            const data = { sku: sku };
            const response = await fetch(url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });

            if (response.ok) {
                console.log("La requête Delete a été envoyée avec succès.");
                window.location.href = "/sale";
            } else {
                console.error("Erreur lors de l'envoi de la requête Delete.");
            }
        } catch (error) {
            console.error("Erreur lors de l'envoi de la requête Delete:", error);
        }
    });
});

updateButtons.forEach((updateButton) => {
    updateButton.addEventListener("click", async function () {
        try {
            const sku = updateButton.getAttribute("data-sku");
            let quantity = prompt("Entrez la quantité désirée");

            if (!/^\d+$/.test(quantity)) {
                const error = "La quantité ne peut contenir que des chiffres.";
                return alert(error);
            }

            quantity = parseInt(quantity);

            if (isNaN(quantity) || quantity < 1 || quantity % 1 !== 0 || quantity === "") {
                const error = "La quantité doit être un nombre entier positif.";
                return alert(error);
            }

            const url = "/line/update";
            const data = { sku: sku, quantity: quantity };
            const response = await fetch(url, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(data),
            });

            if (response.ok) {
                console.log("La requête POST a été envoyée avec succès.");
                window.location.href = "/sale";
            } else {
                console.error("Erreur lors de l'envoi de la requête POST.");
            }
        } catch (error) {
            console.error("Erreur lors de l'envoi de la requête POST:", error);
        }
    });
});
