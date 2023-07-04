import "./style.css";
//Récupération des éléments du DOM
const addButton = document.querySelector(".add-card") as HTMLButtonElement;

class Card {
    id: string;
    position: string;
    name: string;
    height: {
        feet: string;
        inches: string;
    };
    age: string;
    nbPartie: string;
    nbPoint: string;
    number: string;
    pays: string;
    constructor(id: string, position: string, name: string, height: { feet: string; inches: string }, age: string, nbPartie: string, nbPoint: string, number: string, pays: string) {
        this.id = id;
        this.position = position;
        this.name = name;
        this.height = height;
        this.age = age;
        this.nbPartie = nbPartie;
        this.nbPoint = nbPoint;
        this.number = number;

        this.pays = pays;
    }
}

// const cardList: Card[] = localStorage.getItem("cardList") ? JSON.parse(localStorage.getItem("cardList")) : [];
const cardList: Card[] = JSON.parse(localStorage.getItem("cardList") ?? "[]");

if (!localStorage.getItem("cardList")) {
    const card1: Card = new Card("1", "Ailier G", "Alex Ovechkin", { feet: "6", inches: "3" }, "1985-09-17", "1140", "1140", "8", "russie");
    const card2: Card = new Card("2", "Centre", "Wayne Gretzky", { feet: "6", inches: "0" }, "1961-01-26", "1487", "2857", "99", "canada");
    const card3: Card = new Card("3", "Ailier D", "Jaromir Jagr", { feet: "6", inches: "3" }, "1972-02-15", "1733", "1921", "68", "republiquetcheque");
    const card4: Card = new Card("4", "Ailier D", "Teemu Selanne", { feet: "6", inches: "0" }, "1970-07-03", "1451", "1457", "8", "finlande");
    const card5: Card = new Card("5", "Centre", "Mario Lemieux", { feet: "6", inches: "4" }, "1965-10-05", "915", "1723", "66", "canada");
    const card6: Card = new Card("6", "Défenseur", "Bobby Orr", { feet: "6", inches: "0" }, "1948-03-20", "915", "915", "2", "canada");
    const card7: Card = new Card("7", "Centre", "Sidney Crosby", { feet: "5", inches: "11" }, "1987-08-07", "1270", "1288", "51", "canada");
    const card8: Card = new Card("8", "Ailier G", "Henrik Lundqvist", { feet: "6", inches: "1" }, "1982-03-02", "887", "887", "0", "suede");
    const card9: Card = new Card("9", "Centre", "Connor McDavid", { feet: "6", inches: "1" }, "1997-01-13", "574", "574", "1", "canada");
    const card10: Card = new Card("10", "Ailier G", "Patrick Kane", { feet: "5", inches: "10" }, "1988-11-19", "1072", "1072", "0", "usa");

    cardList.push(card1, card2, card3, card4, card5, card6, card7, card8, card9, card10);
    localStorage.setItem("cardList", JSON.stringify(cardList));
}

// Fonction pour initialiser le formulaire
const initForm = (cardDiv: HTMLElement) => {
    const positionSelect = cardDiv.querySelector("#position") as HTMLSelectElement;
    const nameInput = cardDiv.querySelector('.name input[type="text"]') as HTMLInputElement;
    const heightInputFeet = cardDiv.querySelector('.height input[type="number"]:nth-child(2)') as HTMLInputElement;
    const heightInputInches = cardDiv.querySelector('.height input[type="number"]:nth-child(4)') as HTMLInputElement;
    const ageInput = cardDiv.querySelector(".input-date") as HTMLInputElement;
    const nbPartieInput = cardDiv.querySelector('.nb-partie input[type="number"]') as HTMLInputElement;
    const nbPointInput = cardDiv.querySelector('.nb-point input[type="number"]') as HTMLInputElement;
    const numberInput = cardDiv.querySelector('.number input[type="number"]') as HTMLInputElement;
    const cardFront = cardDiv.querySelector(".card") as HTMLElement;
    const cardback = cardDiv.querySelector(".card-back") as HTMLElement;
    const positionElement = cardFront.querySelector(".position p") as HTMLElement;
    const nameElement = cardFront.querySelector(".name span") as HTMLElement;
    const heightInches = cardFront.querySelector("#inches") as HTMLElement;
    const heightFeet = cardFront.querySelector("#feets") as HTMLElement;
    const ageElement = cardFront.querySelector(".date-naissance span") as HTMLElement;
    const nbPartieElement = cardFront.querySelector(".nb-partie span") as HTMLElement;
    const nbPointElement = cardFront.querySelector(".nb-point span") as HTMLElement;
    const numberElement = cardFront.querySelector(".number p") as HTMLElement;
    const moyennePoint = cardback.querySelector(".moyenne-point span") as HTMLElement;
    const paysElement = cardFront.querySelector(".pays") as HTMLElement;
    const paysSelect = cardDiv.querySelector("#pays") as HTMLSelectElement;
    const pays = paysElement.dataset.pays as string;

    // Initialisation des valeurs du formulaire
    positionSelect.value = positionElement.textContent || "";
    nameInput.value = nameElement.textContent || "";
    heightInputInches.value = heightInches.textContent || "";
    heightInputFeet.value = heightFeet.textContent || "";
    ageInput.value = ageElement.textContent || "";
    nbPartieInput.value = nbPartieElement.textContent || "";
    nbPointInput.value = nbPointElement.textContent || "";
    numberInput.value = numberElement.textContent || "";
    paysSelect.value = pays;
    nbPointInput.addEventListener("input", function () {
        if (nbPartieInput.value !== "0") {
            moyennePoint.textContent = (+nbPointInput.value / +nbPartieInput.value).toFixed(2);
        } else {
            moyennePoint.textContent = "0";
        }
    });
    nbPartieInput.addEventListener("input", function () {
        if (nbPartieInput.value !== "0") {
            moyennePoint.textContent = (+nbPointInput.value / +nbPartieInput.value).toFixed(2);
        }
    });
};
// Fonction pour mettre à jour la carte
const updateCard = (cardDiv: HTMLElement) => {
    const cardFront = cardDiv.querySelector(".card");
    if (cardFront) {
        const positionElement = cardFront.querySelector(".position p") as HTMLElement;
        const nameElement = cardFront.querySelector(".name span") as HTMLElement;
        const heightInches = cardFront.querySelector("#inches") as HTMLElement;
        const heightFeet = cardFront.querySelector("#feets") as HTMLElement;
        const ageElement = cardFront.querySelector(".date-naissance span") as HTMLElement;
        const nbPartieElement = cardFront.querySelector(".nb-partie span") as HTMLElement;
        const nbPointElement = cardFront.querySelector(".nb-point span") as HTMLElement;
        const numberElement = cardFront.querySelector(".number p") as HTMLElement;
        const positionSelect = cardDiv.querySelector("#position") as HTMLSelectElement;
        const nameInput = cardDiv.querySelector('.name input[type="text"]') as HTMLInputElement;
        const heightInputFeet = cardDiv.querySelector('.height input[type="number"]:nth-child(2)') as HTMLInputElement;
        const heightInputInches = cardDiv.querySelector('.height input[type="number"]:nth-child(4)') as HTMLInputElement;
        const ageInput = cardDiv.querySelector(".input-date") as HTMLInputElement;
        const nbPartieInput = cardDiv.querySelector('.nb-partie input[type="number"]') as HTMLInputElement;
        const nbPointInput = cardDiv.querySelector('.nb-point input[type="number"]') as HTMLInputElement;
        const numberInput = cardDiv.querySelector('.number input[type="number"]') as HTMLInputElement;
        const moyenneElement = cardFront.querySelector(".moyenne-point span") as HTMLElement;
        const paysElement = cardFront.querySelector(".pays") as HTMLElement;
        // const pays = paysElement.dataset.pays;
        const paysSelect = cardDiv.querySelector("#pays") as HTMLSelectElement;

        //validation du formulaire
        if (nameInput.value.length < 3) {
            alert("Veuillez entrer un nom d'une longueur minimale de 3 caractères");
            return;
        }
        if (+heightInputFeet.value < 4 || +heightInputFeet.value > 8) {
            alert("Veuillez entrer une taille en pieds entre 4 et 8");
            return;
        }
        if (+heightInputInches.value < 0 || +heightInputInches.value > 11) {
            alert("Veuillez entrer une taille en pouces entre 0 et 11");
            return;
        }
        const birthDate = new Date(ageInput.value);
        const todayDate = new Date();
        if (birthDate > todayDate) {
            alert("Veuillez entrer une date de naissance valide");
            return;
        }
        if (+nbPartieInput.value < 0) {
            alert("Veuillez entrer un nombre de partie valide");
            return;
        }
        if (+nbPointInput.value < 0) {
            alert("Veuillez entrer un nombre de point valide");
            return;
        }
        if (+numberInput.value < 0 || +numberInput.value > 99) {
            alert("Veuillez entrer un numéro de maillot entre 0 et 99");
            return;
        }

        // Mise à jour des valeurs de la carte
        paysElement.dataset.pays = paysSelect.value as string;
        paysElement.setAttribute("src", `images/${paysSelect.value}.png`);
        paysElement.setAttribute("alt", paysSelect.value);
        paysElement.textContent = paysSelect.value as string;
        positionElement.textContent = positionSelect.value as string;
        nameElement.textContent = nameInput.value as string;
        heightInches.textContent = heightInputInches.value as string;
        heightFeet.textContent = heightInputFeet.value as string;
        ageElement.textContent = ageInput.value as string;
        nbPartieElement.textContent = nbPartieInput.value as string;
        nbPointElement.textContent = nbPointInput.value as string;
        numberElement.textContent = numberInput.value as string;

        if (nbPartieInput.value !== "0") {
            moyenneElement.textContent = (+nbPointInput.value / +nbPartieInput.value).toFixed(2);
        } else {
            moyenneElement.textContent = "0";
        }
        //update cardList and localStorage
        const id = cardDiv.getAttribute("id");
        const card: Card | undefined = cardList.find((c) => c.id === id);
        if (card) {
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
            cardDiv.classList.remove("card-open");
            cardDiv.style.transform = `translateX(0px) translateY(0px) scale(1,1)`;
            cardDiv.firstElementChild?.classList.add("hover-effect");
            cardDiv.style.zIndex = "60";
            const closeButton = cardDiv.querySelector(".close");
            closeButton?.setAttribute("style", "display: none");
        }
    } else {
        alert("Erreur lors de la mise à jour de la carte");
    }
};

// Ajoute un event listener sur chaque carte pour l'ouvrir
const ajoutOuverture = (cardDiv: HTMLElement) => {
    const closeButton = cardDiv.querySelector(".close");
    closeButton?.setAttribute("style", "display: none");
    cardDiv.addEventListener("click", function () {
        // Si la carte est ouverte, on ne fait rien
        if (this.classList.contains("card-open")) {
            return;
        }
        const close = this.querySelector(".close");
        close?.setAttribute("style", "display: block");
        let closeOther = document.querySelectorAll(".close");
        closeOther.forEach(function (card) {
            if (card != close && card) {
                card.setAttribute("style", "display: none");
            }
        });
        const openCard = document.querySelector(".card-open");
        const cardWidth = this.offsetWidth / 2;
        const cardHeight = this.offsetHeight / 2;
        const cord = this.getBoundingClientRect(); // position de la carte
        const windowWidth = window.innerWidth;
        const windowHeight = window.innerHeight;
        // Calcul de la position à laquelle la carte doit être déplacée pour être centrée
        const x = windowWidth / 2 - cardWidth - cord.left;
        const y = windowHeight / 2 - cardHeight - cord.top;
        this.style.transform = `translateX(${x}px) translateY(${y}px) scale(2,2) `;
        initForm(this);
        this.classList.add("card-open");
        this.firstElementChild?.classList.remove("hover-effect");
        this.style.zIndex = "70";
        // Ferme la carte ouverte si une autre carte est cliquée
        if (openCard && openCard != this) {
            openCard.classList.remove("card-open");
            openCard.setAttribute("style", "transform: translateX(0px) translateY(0px) scale(1,1)");
            openCard.firstElementChild?.classList.add("hover-effect");
            openCard.setAttribute("style", "z-index: 60");
        }
    });
};
// ajoute des event listeners sur les cartes
const ajoutEvent = (cardDiv: HTMLElement) => {
    const closeCard = (cardDiv: HTMLElement) => {
        cardDiv.classList.remove("card-open");
        cardDiv.style.transform = `translateX(0px) translateY(0px) scale(1,1)`;
        cardDiv.firstElementChild?.classList.add("hover-effect");
        cardDiv.style.zIndex = "60";
        updateCard(cardDiv);
    };

    // Ajout d'un événement click au bouton de fermeture de la carte
    const closeButton = cardDiv.querySelector(".close") as HTMLElement;
    const closeBack = cardDiv.querySelector(".close-back") as HTMLElement;
    closeButton.addEventListener("click", function (event) {
        event.stopPropagation();
        closeButton.setAttribute("style", "display: none");
        closeCard(cardDiv);
    });
    // Ajout d'un événement click au bouton de retour à la liste de cartes
    closeBack.addEventListener("click", function (event) {
        event.stopPropagation();
        closeCard(cardDiv);
        closeButton.setAttribute("style", "display: none");
    });
    // Ajout d'un événement click pour retourner la carte et afficher le formulaire
    cardDiv.addEventListener("click", function (event: Event) {
        const target = event.target as HTMLElement;
        if (target.classList.contains("modify")) {
            let x = this.style.transform;
            this.style.transform = `${x} rotateY(180deg)`;
            const nameInput = cardDiv.querySelector(".name-input") as HTMLInputElement;
            nameInput.focus();
        }
    });
    const removeCard = (cardDiv: HTMLElement) => {
        const cardList = JSON.parse(localStorage.getItem("cardList") ?? "[]");
        const cardId = cardDiv.getAttribute("id");
        const index = cardList.findIndex((card: Card) => card.id === cardId);
        cardList.splice(index, 1);
        localStorage.setItem("cardList", JSON.stringify(cardList));
        cardDiv.remove();
    };

    // Ajout d'un événement click pour supprimer la carte e
    const deleteButton = cardDiv.querySelector(".delete") as HTMLElement;
    deleteButton.addEventListener("click", function (event) {
        event.stopPropagation();
        removeCard(cardDiv);
    });

    // Ajout d'un événement click valider le formulaire
    const validateButton = cardDiv.querySelector("#valider") as HTMLElement;
    validateButton.addEventListener("click", function (event) {
        event.stopPropagation();
        updateCard(cardDiv);
    });
};
const createCard = (card: Card | {}) => {
    const defaultCard: Card = {
        id: Math.random().toString(36).substr(2, 9) + "-" + Date.now(),
        name: "Jonh Doe",
        position: "Centre",
        pays: "canada",
        height: { feet: "6", inches: "8" },
        age: "1994-07-07",
        nbPartie: "1",
        nbPoint: "1",
        number: "23",
    };

    const mergedCard = { ...defaultCard, ...card };
    const newCard = document.createElement("div");
    newCard.classList.add("flip-card");
    newCard.setAttribute("id", mergedCard.id);
    newCard.innerHTML = `
        <div class="card hover-effect shadow">
            <div  class="card-top">
                <div>
                    <img class="close" src="images/x-symbol.png" />
                </div>
                <div class="position">
                    <p>${mergedCard.position}</p>
                </div>
                <img data-pays="${mergedCard.pays}" src="./images/${mergedCard.pays}.png" class="pays" />
            </div>
            <div class="card-bottom">
                <div class="name">
                    <p><span>${mergedCard.name}</span> <img class="modify" src="images/edit-new-icon-22.png" alt="" /> <img class="delete" src="images/delete.png" alt="" /></p>
                </div>
                <div class="height">
                <p>Grandeur: <span id="feets">${mergedCard.height.feet}</span>,<span id="inches">${mergedCard.height.inches}</span> '</p>

                </div>
                <div class="date-naissance">
                    <p>Age: <span>${mergedCard.age}</span></p>
                </div>
                <div class="nb-partie">
                    <p>Nb partie: <span>${mergedCard.nbPartie}</span></p>
                </div>
                <div class="nb-point">
                    <p>Nb points: <span>${mergedCard.nbPoint}</span></p>
                </div>
                <div class="moyenne-point">
                <p>Moyenne points/match: <span>${(+mergedCard.nbPoint / +mergedCard.nbPartie).toFixed(2)}</span></p>
                </div>
                <div class="number">
                    <p>${mergedCard.number}</p>
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
                            <option class="option" value="Ailier G">Ailier G</option>
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
                <p>Moyenne points/match: <span>${mergedCard.nbPoint && mergedCard.nbPartie ? (+mergedCard.nbPoint / +mergedCard.nbPartie).toFixed(2) : "1"}</span></p>
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

const addCard = (card: Card) => {
    const newCard = createCard(card);
    const addCardBtn = document.querySelector(".add-card");
    const addCardDiv = addCardBtn?.parentNode;
    addCardDiv?.insertBefore(newCard, addCardBtn);
    ajoutEvent(newCard);
    ajoutOuverture(newCard);
};

cardList.forEach((card) => {
    addCard(card);
});
const ajoubutton = function (addButton: HTMLElement) {
    addButton.addEventListener("click", function () {
        const addCardBtn = document.querySelector(".add-card") as HTMLElement;
        const addCardDiv = addCardBtn.parentNode as HTMLElement;
        const newCard = createCard({});
        const id = newCard.getAttribute("id");
        const name = newCard.querySelector(".name .modify") as HTMLInputElement;
        // const position = newCard.querySelector(".position .modify") as  HTMLSelectElement;
        const position = newCard.querySelector(".position-select") as HTMLSelectElement;
        const pays = newCard.querySelector(".pays-select") as HTMLSelectElement;
        const height = newCard.querySelector(".height input") as HTMLInputElement;
        const age = newCard.querySelector(".date-naissance input") as HTMLInputElement;
        const nbPartie = newCard.querySelector(".nb-partie input") as HTMLInputElement;
        const nbPoint = newCard.querySelector(".nb-point input") as HTMLInputElement;
        const number = newCard.querySelector(".number input") as HTMLInputElement;
        addCardDiv.insertBefore(newCard, addCardBtn);
        ajoutEvent(newCard);
        ajoutOuverture(newCard);
        newCard.click();
        const nameModify = newCard.querySelector(".name .modify") as HTMLElement;
        nameModify.click();
        const cardToSave: Card = {
            id: id || "",
            name: name.textContent || "",
            position: position.value,
            pays: pays.value,
            height: {
                feet: height.value,
                inches: height.value,
            },
            age: age.value,
            nbPartie: nbPartie.value,
            nbPoint: nbPoint.value,
            number: number.value,
        };
        cardList.push(cardToSave);
        localStorage.setItem("cardList", JSON.stringify(cardList));
    });
};
ajoubutton(addButton);

// Ajout d'un event listener pour trier les cartes par nombre de points
const sortSwitch = document.querySelector("#switch") as HTMLInputElement;
sortSwitch.addEventListener("change", handleSort);

function handleSort() {
    const cardList = JSON.parse(localStorage.getItem("cardList") ?? "[]");
    const sortedCardList = sortSwitch.checked ? cardList.slice().sort((a: Card, b: Card) => +b.nbPoint - +a.nbPoint) : cardList;

    updateCardList(sortedCardList);
}

// Gestionnaire d'événements de recherche
const searchInput = document.getElementById("search-input") as HTMLInputElement;
searchInput.addEventListener("input", handleSearch);

function handleSearch() {
    const searchText = searchInput.value.toLowerCase();
    const cardList = JSON.parse(localStorage.getItem("cardList") ?? "[]");
    const filteredCards: Card[] = cardList.filter((card: Card) => card.name.toLowerCase().includes(searchText) || card.position.toLowerCase().includes(searchText) || card.pays.toLowerCase().includes(searchText));

    updateCardList(filteredCards);
}

// Fonction pour mettre à jour la liste des cartes affichées
function updateCardList(cards: Card[]) {
    const cardContainer = document.querySelector(".card-container") as HTMLElement;
    cardContainer.innerHTML = ` <div class="add-card hover-effect">
    <span>+</span>
  </div>`;
    const addCardBtn = document.querySelector(".add-card") as HTMLElement;
    ajoubutton(addCardBtn);

    cards.forEach((card: Card) => {
        addCard(card);
    });
}
