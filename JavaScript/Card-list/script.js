//Récupération des éléments du DOM
const flipCards = document.querySelectorAll(".flip-card");
const cardList = localStorage.getItem("cardList") ? JSON.parse(localStorage.getItem("cardList")) : [];
const addButton = document.querySelector(".add-card");
if (!localStorage.getItem("cardList")) {
    cardList.push(
        {
            position: "Ailier D",
            name: "Jaromir Jagr",
            height: {
                feet: "6",

                inches: "3",
            },
            age: "1972-02-15",
            nbPartie: "1733",
            nbPoint: "1921",
            number: "68",
            cardId: "test2",
            pays: "republiquetcheque",
        },
        {
            position: "Ailier D",
            name: "Teemu Selanne",
            height: {
                feet: "6",
                inches: "0",
            },
            age: "1970-07-03",
            nbPartie: "1451",
            nbPoint: "1457",
            number: "8",
            cardId: "test3",
            pays: "finlande",
        },
        {
            position: "Centre",
            name: "Mario Lemieux",
            height: {
                feet: "6",
                inches: "4",
            },
            age: "1965-10-05",
            nbPartie: "915",
            nbPoint: "1723",
            number: "66",
            cardId: "test4",
            pays: "canada",
        },

        {
            position: "Ailier G",
            name: "Alex Ovechkin",
            height: {
                feet: "6",
                inches: "3",
            },
            age: "1985-09-17",
            nbPartie: "1140",
            nbPoint: "1140",
            number: "8",
            cardId: "test5",
            pays: "russie",
        },
        {
            position: "Centre",
            name: "Wayne Gretzky",
            height: {
                feet: "6",
                inches: "0",
            },
            age: "1961-01-26",
            nbPartie: "1487",
            nbPoint: "2857",
            number: "99",
            cardId: "test1",
            pays: "canada",
        }
    );
    localStorage.setItem("cardList", JSON.stringify(cardList));
}

// Fonction pour initialiser le formulaire
const initForm = (c) => {
    const positionSelect = c.querySelector("#position");
    const nameInput = c.querySelector('.name input[type="text"]');
    const heightInputFeet = c.querySelector('.height input[type="number"]:nth-child(2)');
    const heightInputInches = c.querySelector('.height input[type="number"]:nth-child(4)');
    const ageInput = c.querySelector(".input-date");
    const nbPartieInput = c.querySelector('.nb-partie input[type="number"]');
    const nbPointInput = c.querySelector('.nb-point input[type="number"]');
    const numberInput = c.querySelector('.number input[type="number"]');
    const cardFront = c.querySelector(".card");
    const cardback = c.querySelector(".card-back");
    const positionElement = cardFront.querySelector(".position p");
    const nameElement = cardFront.querySelector(".name span");
    const heightInches = cardFront.querySelector("#inches");
    const heightFeet = cardFront.querySelector("#feets");
    const ageElement = cardFront.querySelector(".date-naissance span");
    const nbPartieElement = cardFront.querySelector(".nb-partie span");
    const nbPointElement = cardFront.querySelector(".nb-point span");
    const numberElement = cardFront.querySelector(".number p");
    const moyennePoint = cardback.querySelector(".moyenne-point span");
    const paysElement = cardFront.querySelector(".pays");
    const paysSelect = c.querySelector("#pays");
    const pays = paysElement.dataset.pays;

    // Initialisation des valeurs du formulaire
    positionSelect.value = positionElement.textContent;
    nameInput.value = nameElement.textContent;
    heightInputInches.value = heightInches.textContent;
    heightInputFeet.value = heightFeet.textContent;
    ageInput.value = ageElement.textContent;
    nbPartieInput.value = nbPartieElement.textContent;
    nbPointInput.value = nbPointElement.textContent;
    numberInput.value = numberElement.textContent;
    paysSelect.value = pays;
    nbPointInput.addEventListener("input", function () {
        if (nbPartieInput.value !== "0") {
            moyennePoint.textContent = (nbPointInput.value / nbPartieInput.value).toFixed(2);
        } else {
            moyennePoint.textContent = "0";
        }
    });
    nbPartieInput.addEventListener("input", function () {
        if (nbPartieInput.value !== "0") {
            moyennePoint.textContent = (nbPointInput.value / nbPartieInput.value).toFixed(2);
        }
    });
};
// Fonction pour mettre à jour la carte
const updateCard = (c) => {
    const cardFront = c.querySelector(".card");
    const positionElement = cardFront.querySelector(".position p");
    const nameElement = cardFront.querySelector(".name span");
    const heightInches = cardFront.querySelector("#inches");
    const heightFeet = cardFront.querySelector("#feets");
    const ageElement = cardFront.querySelector(".date-naissance span");
    const nbPartieElement = cardFront.querySelector(".nb-partie span");
    const nbPointElement = cardFront.querySelector(".nb-point span");
    const numberElement = cardFront.querySelector(".number p");
    const positionSelect = c.querySelector("#position");
    const nameInput = c.querySelector('.name input[type="text"]');
    const heightInputFeet = c.querySelector('.height input[type="number"]:nth-child(2)');
    const heightInputInches = c.querySelector('.height input[type="number"]:nth-child(4)');
    const ageInput = c.querySelector(".input-date");
    const nbPartieInput = c.querySelector('.nb-partie input[type="number"]');
    const nbPointInput = c.querySelector('.nb-point input[type="number"]');
    const numberInput = c.querySelector('.number input[type="number"]');
    const moyenneElement = cardFront.querySelector(".moyenne-point span");
    const paysElement = cardFront.querySelector(".pays");
    const pays = paysElement.dataset.pays;
    const paysSelect = c.querySelector("#pays");

    //validation du formulaire
    if (nameInput.value.length < 3) {
        alert("Veuillez entrer un nom d'une longueur minimale de 3 caractères");
        return;
    }
    if (heightInputFeet.value < 4 || heightInputFeet.value > 8) {
        alert("Veuillez entrer une taille en pieds entre 4 et 8");
        return;
    }
    if (heightInputInches.value < 0 || heightInputInches.value > 11) {
        alert("Veuillez entrer une taille en pouces entre 0 et 11");
        return;
    }
    birthDate = new Date(ageInput.value);
    todayDate = new Date();
    if (birthDate > todayDate) {
        alert("Veuillez entrer une date de naissance valide");
        return;
    }
    if (nbPartieInput.value < 0) {
        alert("Veuillez entrer un nombre de partie valide");
        return;
    }
    if (nbPointInput.value < 0) {
        alert("Veuillez entrer un nombre de point valide");
        return;
    }
    if (numberInput.value < 0 || numberInput.value > 99) {
        alert("Veuillez entrer un numéro de maillot entre 0 et 99");
        return;
    }

    // Mise à jour des valeurs de la carte
    paysElement.dataset.pays = paysSelect.value;
    paysElement.src = `images/${paysSelect.value}.png`;
    paysElement.textContent = paysSelect.value;
    positionElement.textContent = positionSelect.value;
    nameElement.textContent = nameInput.value;
    heightInches.textContent = heightInputInches.value;
    heightFeet.textContent = heightInputFeet.value;
    ageElement.textContent = ageInput.value;
    nbPartieElement.textContent = nbPartieInput.value;
    nbPointElement.textContent = nbPointInput.value;
    numberElement.textContent = numberInput.value;

    if (nbPartieInput.value !== "0") {
        moyenneElement.textContent = (nbPointInput.value / nbPartieInput.value).toFixed(2);
    } else {
        moyenneElement.textContent = "0";
    }
    //update cardList and localStorage
    const cardId = c.getAttribute("id");
    const card = cardList.find((c) => c.cardId === cardId);
    card.position = positionSelect.value;
    card.name = nameInput.value;
    card.height.feet = heightInputFeet.value;
    card.height.inches = heightInputInches.value;
    card.age = ageInput.value;
    card.nbPartie = nbPartieInput.value;
    card.nbPoint = nbPointInput.value;
    card.number = numberInput.value;
    card.pays = paysSelect.value;
    localStorage.setItem("cardList", JSON.stringify(cardList));
    // Ferme la carte
    c.classList.remove("card-open");
    c.style.transform = `translateX(0px) translateY(0px) scale(1,1)`;
    c.firstElementChild.classList.add("hover-effect");
    c.style.zIndex = "60";
    const closeButton = c.querySelector(".close");
    closeButton.style.display = "none";
};

// Ajoute un event listener sur chaque carte pour l'ouvrir
ajoutOuverture = (c) => {
    const closeButton = (c.querySelector(".close").style.display = "none");
    c.addEventListener("click", function () {
        // Si la carte est ouverte, on ne fait rien
        if (this.classList.contains("card-open")) {
            return;
        }
        const close = this.querySelector(".close");
        close.style.display = "block";
        let closeOther = document.querySelectorAll(".close");
        closeOther.forEach(function (c) {
            if (c != close) {
                c.style.display = "none";
            }
        });
        const openCard = document.querySelector(".card-open");
        const cardWidth = this.offsetWidth / 2;
        const cardHeight = this.offsetHeight / 2;
        const cord = this.getBoundingClientRect(); // position de la carte
        const bodyWidth = document.body.offsetWidth;
        const bodyHeight = document.body.offsetHeight;
        // Calcul de la position à laquelle la carte doit être déplacée pour être centrée
        const x = bodyWidth / 2 - cardWidth - cord.left;
        const y = bodyHeight / 2 - cardHeight - cord.top;
        this.style.transform = `translateX(${x}px) translateY(${y}px) scale(2,2) `;
        initForm(this);
        this.classList.add("card-open");
        this.firstElementChild.classList.remove("hover-effect");
        this.style.zIndex = "70";
        // Ferme la carte ouverte si une autre carte est cliquée
        if (openCard) {
            openCard.classList.remove("card-open");
            openCard.style.transform = `translateX(0px) translateY(0px) scale(1,1)`;
            openCard.firstElementChild.classList.add("hover-effect");
            openCard.style.zIndex = "60";
        }
    });
};

const ajoutEvent = (c) => {
    const closeCard = (c) => {
        c.classList.remove("card-open");
        c.style.transform = `translateX(0px) translateY(0px) scale(1,1)`;
        c.firstElementChild.classList.add("hover-effect");
        c.style.zIndex = "60";
    };

    // Ajout d'un événement click au bouton de fermeture de la carte
    const closeButton = c.querySelector(".close");
    const closeBack = c.querySelector(".close-back");
    closeButton.addEventListener("click", function (event) {
        event.stopPropagation();
        closeButton.style.display = "none";
        closeCard(c);
    });
    // Ajout d'un événement click au bouton de retour à la liste de cartes
    closeBack.addEventListener("click", function (event) {
        event.stopPropagation();
        closeCard(c);
        closeButton.style.display = "none";
    });
    // Ajout d'un événement click pour retourner la carte et afficher le formulaire
    c.addEventListener("click", function (event) {
        if (event.target.classList.contains("modify")) {
            let x = this.style.transform;
            this.style.transform = `${x} rotateY(180deg)`;
            c.querySelector(".name-input").focus();
        }
    });
    const removeCard = (c) => {
        const cardList = JSON.parse(localStorage.getItem("cardList"));
        const cardId = c.getAttribute("id");
        const index = cardList.findIndex((card) => card.id === cardId);
        cardList.splice(index, 1);
        localStorage.setItem("cardList", JSON.stringify(cardList));
        c.remove();
    };

    // Ajout d'un événement click pour supprimer la carte e
    const deleteButton = c.querySelector(".delete");
    deleteButton.addEventListener("click", function (event) {
        event.stopPropagation();
        removeCard(c);
    });

    // Ajout d'un événement click valider le formulaire
    const validateButton = c.querySelector("#valider");
    validateButton.addEventListener("click", function (event) {
        event.stopPropagation();
        updateCard(c);
    });
};
createCard = (card = {}) => {
    const uniqueId = Math.random().toString(36).substr(2, 9) + "-" + Date.now();
    const newCard = document.createElement("div");
    newCard.classList.add("flip-card");
    newCard.setAttribute("id", card.cardId ? card.cardId : uniqueId);
    newCard.innerHTML = ` 
        <div class="card hover-effect shadow">
            <div  class="card-top">
                <div>
                    <img class="close" src="images/x-symbol.png" />
                </div>
                <div class="position">
                    <p>${card.position ? card.position : "Centre"}</p>
                </div>
                <img data-pays="${card.pays ? card.pays : "canada"}" src="/images/${card.pays ? card.pays : "canada"}.png" class="pays" />
            </div>
            <div class="card-bottom">
                <div class="name">
                    <p><span>${card.name ? card.name : "John Doe"}</span> <img class="modify" src="images/edit-new-icon-22.png" alt="" /> <img class="delete" src="images/delete.png" alt="" /></p>
                </div>
                <div class="height">
                <p>Grandeur: <span id="feets">${card.height && card.height.feet ? card.height.feet : "6"}</span>,<span id="inches">${card.height && card.height.inches ? card.height.inches : "2"}</span> '</p>

                </div>
                <div class="date-naissance">
                    <p>Age: <span>${card.age ? card.age : "1994-07-07"}</span></p>
                </div>
                <div class="nb-partie">
                    <p>Nb partie: <span>${card.nbPartie ? card.nbPartie : 1}</span></p>
                </div>
                <div class="nb-point">
                    <p>Nb points: <span>${card.nbPoint ? card.nbPoint : 1}</span></p>
                </div>
                <div class="moyenne-point">
                <p>Moyenne points/match: <span>${card.nbPoint && card.nbPartie ? (card.nbPoint / card.nbPartie).toFixed(2) : "1"}</span></p>
                </div>
                <div class="number">
                    <p>${card.number ? card.number : "23"}</p>
                </div>
            </div>
        </div>
        <div class="card-back shadow">
            <div class="card-top">
                <div>
                    <img class="close-back" src="images/x-symbol.png" />
                </div>
                <div class="position">
                    <p>
                        <select class="position-select" name="position" id="position">
                            <option class="option" value="Centre">Centre</option>
                            <option class="option" value="Alier G">Ailier G</option>
                            <option class="option" value="Ailier D">Ailier D</option>
                            <option class="option" value="Defenseur D">Defenseur D</option>
                            <option class="option" value="Defenseur G">Defenseur G</option>
                            <option class="option" value="Gardien">Gardien</option>
                        </select>
                    </p>
                </div>
                <select class="pays-select" name="pays" id="pays">
                    <option class="option" value="canada">Canada</option>
                    <option class="option" value="usa">USA</option>
                    <option class="option" value="suede">Suède</option>
                    <option class="option" value="finlande">Finlande</option>
                    <option class="option" value="russie">Russie</option>
                    <option class="option" value="republiquetcheque">République Tchèque</option>
                    <option class="option" value="suisse">Suisse</option>
                </select>
                
                
            </div>
            <div class="card-bottom">
                <div class="name">
                    <input class="name-input" type="text" name="name-input"/>
                </div>
                <div class="height">
                    <p>
                        Grandeur: <br />
                        <input type="number" min="4" max="8" />pieds <br /><input type="number" min="0" max="11" /> pouces 
                    </p>
                </div>
                <div class="date-naissance">
                    <p>Age: <input class="input-date" type="date" /></p>
                </div>
                <div class="nb-partie">
                    <p>Nb partie: <input type="number" /></p>
                </div>
                <div class="nb-point">
                    <p>Nb points: <input type="number" /></p>
                </div>
                <div class="moyenne-point">
                <p>Moyenne points/match: <span>${card.nbPoint && card.nbPartie ? (card.nbPoint / card.nbPartie).toFixed(2) : "1"}</span></p>
                </div>
                <div class="number">
                    <p><input type="number" /></p>
                    <button class = "btn btn-2" id="valider">Valider</button>
                </div>
            </div>

        `;
    // ajout d'un event listener pour la validation du formulaire
    return newCard;
};
const addCard = (card) => {
    const newCard = createCard(card);
    const addCardBtn = document.querySelector(".add-card");
    const addCardDiv = addCardBtn.parentNode;
    addCardDiv.insertBefore(newCard, addCardBtn);
    ajoutEvent(newCard);
    ajoutOuverture(newCard);
};

cardList.forEach((card) => {
    addCard(card);
});
const ajoubutton = function (addButton) {
    addButton.addEventListener("click", function () {
        const newCard = createCard();
        const addCardBtn = document.querySelector(".add-card");
        const addCardDiv = addCardBtn.parentNode;
        addCardDiv.insertBefore(newCard, addCardBtn);
        ajoutEvent(newCard);
        ajoutOuverture(newCard);
        newCard.click();
        newCard.querySelector(".name .modify").click();
        const cardToSave = {
            name: newCard.querySelector(".name-input").value,
            position: newCard.querySelector(".position-select").value,
            pays: newCard.querySelector(".pays-select").value,
            height: {
                feet: newCard.querySelector(".height input").value,
                inches: newCard.querySelector(".height input").value,
            },
            age: newCard.querySelector(".input-date").value,
            nbPartie: newCard.querySelector(".nb-partie input").value,
            nbPoint: newCard.querySelector(".nb-point input").value,
            number: newCard.querySelector(".number input").value,
            cardId: newCard.id,
        };

        cardList.push(cardToSave);
        localStorage.setItem("cardList", JSON.stringify(cardList));
    });
};
ajoubutton(addButton);

// ajout d'un event listener pour trier les cartes par nombre de points
const sortSwitch = document.querySelector("#switch");
sortSwitch.addEventListener("change", function () {
    const cardListUpdate = JSON.parse(localStorage.getItem("cardList"));
    if (sortSwitch.checked) {
        const sortedCardList = cardListUpdate.slice().sort((a, b) => {
            return b.nbPoint - a.nbPoint;
        });
        const cardContainer = document.querySelector(".card-container");
        cardContainer.innerHTML = ` <div class="add-card hover-effect">
            <span>+</span>
        </div>`;
        const addCardBtn = document.querySelector(".add-card");
        ajoubutton(addCardBtn);
        sortedCardList.forEach((card) => {
            addCard(card);
        });
    } else {
        const cardContainer = document.querySelector(".card-container");
        cardContainer.innerHTML = ` <div class="add-card hover-effect">
        <span>+</span>
    </div>`;
        const addCardBtn = document.querySelector(".add-card");
        ajoubutton(addCardBtn);
        cardListUpdate.forEach((card) => {
            addCard(card);
        });
    }
});
