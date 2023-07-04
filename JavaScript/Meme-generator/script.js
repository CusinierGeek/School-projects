// Déclaration des sélecteurs
const gabarit = document.querySelector("select");
const image = document.getElementById("img-apercu");
const texte = document.getElementById("textarea-etiquette");
const police = document.querySelectorAll("select")[1];
const taille = document.querySelector("input[type=number]");
const couleur = document.querySelector("input[type=color]");
const bandesBlanches = document.querySelector("input[type=checkbox]");
const pTexteMeme = document.querySelectorAll(".p-texte-meme");

// fonction pour trouver l'index de l'étiquette choisie
const getCheckedIndex = () => Array.from(choixEtiquette).findIndex((e) => e.checked);

//fonction pour initialiser le formulaire selon l'étiquette choisie
const init = function (etiquette) {
    texte.value = pTexteMeme[etiquette].textContent;
    police.value = pTexteMeme[etiquette].style.fontFamily;
    taille.value = parseInt(pTexteMeme[etiquette].style.fontSize);
    couleur.value = rgb2hex(pTexteMeme[etiquette].style.color);
};

// Apelle de la fonction init pour initialiser le formulaire au chargement de la page
init(0);

//évenement pour changer l'image
gabarit.addEventListener("change", () => {
    image.src = gabarit.value;
});

//évenement pour changer la position du texte et initialison selon l'étiquette choisie
const choixEtiquette = document.querySelectorAll("input[type=radio]");
for (let i = 0; i < choixEtiquette.length; i++) {
    choixEtiquette[i].addEventListener("change", function () {
        const index = getCheckedIndex(choixEtiquette);
        texte.textContent = pTexteMeme[index].textContent;
        init(index);
    });
}

//évenement pour changer la couleur de l'arrière-plan (bandes blanches)
bandesBlanches.addEventListener("change", () => {
    image.style.backgroundColor = bandesBlanches.checked ? "white" : "transparent";
});

//évenement pour changer le texte de l'étiquette choisie a chaque frappe
texte.addEventListener("keyup", () => {
    const index = getCheckedIndex(choixEtiquette);
    pTexteMeme[index].textContent = texte.value;
});

//évenement pour changer la police
police.addEventListener("change", () => {
    const index = getCheckedIndex(choixEtiquette);
    choixEtiquette[index].checked && (pTexteMeme[index].style.fontFamily = police.value);
});

//évenement pour changer la taille de la police
taille.addEventListener("change", () => {
    const index = getCheckedIndex(choixEtiquette);
    choixEtiquette[index].checked && (pTexteMeme[index].style.fontSize = taille.value + "px");
});

//évenement pour changer la couleur de la police
couleur.addEventListener("input", () => {
    const index = getCheckedIndex(choixEtiquette);
    choixEtiquette[index].checked && (pTexteMeme[index].style.color = couleur.value);
});

// Lorsqu'on recupère la couleur d'un paragraphe, (p.style.color),
// le format retourné est rgb(x, y, x).
// Pour fournir la valeur au input color, on doit convertir au format #RRGGBB
// Aidez-vous de cette fonction.
function rgb2hex(color) {
    // https://stackoverflow.com/a/30381663
    if (color.indexOf("#") != -1) {
        return color;
    }
    color = color.replace("rgba", "").replace("rgb", "").replace("(", "").replace(")", "");
    color = color.split(","); // get Array["R","G","B"]

    // 0) add leading #
    // 1) add leading zero, so we get 0XY or 0X
    // 2) append leading zero with parsed out int value of R/G/B
    //    converted to HEX string representation
    // 3) slice out 2 last chars (get last 2 chars) =>
    //    => we get XY from 0XY and 0X stays the same
    return "#" + ("0" + parseInt(color[0], 10).toString(16)).slice(-2) + ("0" + parseInt(color[1], 10).toString(16)).slice(-2) + ("0" + parseInt(color[2], 10).toString(16)).slice(-2);
}

// Lorsqu'on recupère la couleur d'un paragraphe, (p.style.color),
// le format retourné est rgb(x, y, x).
// Pour fournir la valeur au input color, on doit convertir au format #RRGGBB
// Aidez-vous de cette fonction.
function rgb2hex(color) {
    // https://stackoverflow.com/a/30381663
    if (color.indexOf("#") != -1) {
        return color;
    }

    color = color.replace("rgba", "").replace("rgb", "").replace("(", "").replace(")", "");
    color = color.split(","); // get Array["R","G","B"]

    // 0) add leading #
    // 1) add leading zero, so we get 0XY or 0X
    // 2) append leading zero with parsed out int value of R/G/B
    //    converted to HEX string representation
    // 3) slice out 2 last chars (get last 2 chars) =>
    //    => we get XY from 0XY and 0X stays the same
    return "#" + ("0" + parseInt(color[0], 10).toString(16)).slice(-2) + ("0" + parseInt(color[1], 10).toString(16)).slice(-2) + ("0" + parseInt(color[2], 10).toString(16)).slice(-2);
}
