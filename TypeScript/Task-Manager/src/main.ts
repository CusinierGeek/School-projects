////////////Liens utiles ////////////
/* 
    1. insertRow() : Ajoute une nouvelle ligne à la table et renvoie une référence à la ligne nouvellement créée.
    2. insertCell() : Ajoute une nouvelle cellule à la ligne et renvoie une référence à la cellule nouvellement créée.
    link : https://developer.mozilla.org/fr/docs/Web/API/HTMLTableElement/insertRow
           https://developer.mozilla.org/fr/docs/Web/API/HTMLTableRowElement/insertCell
  

*/

import "./style.css";

// Récupération des éléments du DOM
const titleInput = document.getElementById("title-input") as HTMLInputElement;
const descriptionInput = document.getElementById("description-input") as HTMLInputElement;
const addTaskBtn = document.getElementById("addTaskBtn") as HTMLButtonElement;
const tasksTableHeader = document.getElementById("tasksTableHeader") as HTMLTableSectionElement;
const tasksTable = document.getElementById("tasksTable") as HTMLTableSectionElement;

titleInput.addEventListener("input", (): void => {
    localStorage.setItem("titleValue", titleInput.value);
});

descriptionInput.addEventListener("input", (): void => {
    localStorage.setItem("descriptionValue", descriptionInput.value);
});

// Écouteur d'événement pour le bouton "Add Task"
addTaskBtn.addEventListener("click", (): void => {
    tracker.addTask(titleInput.value, descriptionInput.value);
});

// Classe Task représentant une tâche
class Task {
    static nextId: number = 1;
    id: number;
    title: string;
    description: string;
    completed: boolean;
    lastModified: string;

    constructor(title: string, description: string, completed: boolean = false, lastModified: string = new Date().toLocaleString(), id: number = Task.nextId++) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.completed = completed;
        this.lastModified = lastModified;
    }
}

// Classe TaskTracker pour gérer les tâches
class TaskTracker {
    private tasks: Task[] = [];

    constructor() {
        // Récupération des tâches depuis le stockage local (localStorage)
        this.loadTasksFromLocalStorage();
        this.loadNextIdToLocalStorage();
    }

    public addTask(title: string, description: string): void {
        // Ajout d'une nouvelle tâche
        const task: Task = new Task(title, description);
        this.tasks.push(task);
        this.saveTasksToLocalStorage();
        this.saveNextIdToLocalStorage();
        this.showTasks();
    }

    public updateTaskStatus(id: number, completed: boolean): void {
        // Mise à jour du statut (complété ou non) d'une tâche
        const task: Task | undefined = this.getTaskById(id);
        if (task) {
            task.completed = completed;
            task.lastModified = new Date().toLocaleString();
            this.saveTasksToLocalStorage();
        }
    }

    public editTask(id: number, title: string, description: string): void {
        const task: Task | undefined = this.getTaskById(id);
        if (task) {
            task.title = title;
            task.description = description;
            task.lastModified = new Date().toLocaleString();
            this.saveTasksToLocalStorage();
            this.showTasks();
        }
    }

    public deleteTask(id: number): void {
        const index: number = this.tasks.findIndex((task: Task) => task.id === id);
        if (index !== -1) {
            const deletedRow = tasksTable.querySelector(`#taskRow-${id}`);
            if (deletedRow) {
                deletedRow.classList.add("deleted");
                setTimeout(() => {
                    // laisse le temps à l'animation de se terminer
                    this.tasks.splice(index, 1);
                    this.saveTasksToLocalStorage();
                    this.showTasks();
                }, 200);
            }
        }
    }

    public showTasks(): void {
        // Affichage des tâches dans le tableau HTML
        tasksTable.innerHTML = "";
        if (this.tasks.length === 0) {
            tasksTableHeader.innerHTML = "";
        } else {
            tasksTableHeader.innerHTML = `<th>Task Id</th><th>Title</th><th>Description</th><th>Completed</th><th>Last Modified</th><th>Update/Delete</th>`;
            this.tasks.forEach((task: Task) => {
                // Création d'une nouvelle ligne dans le tableau
                const row = tasksTable.insertRow();
                row.id = `taskRow-${task.id}`;
                row.classList.add("task-row");
                if (task.completed) {
                    row.classList.add("completed");
                }

                // Ajout des cellules dans la ligne avec les valeurs de chaque propriété de la tâche
                row.insertCell().textContent = task.id.toString();
                row.insertCell().textContent = task.title;
                row.insertCell().textContent = task.description;

                const completedCell = row.insertCell();
                const checkboxInput = document.createElement("input");
                checkboxInput.type = "checkbox";
                checkboxInput.checked = task.completed;
                completedCell.appendChild(checkboxInput);
                checkboxInput.addEventListener("change", () => {
                    row.classList.toggle("completed");
                    this.updateTaskStatus(task.id, checkboxInput.checked);
                });

                row.insertCell().textContent = task.lastModified;

                const updateDeleteCell = row.insertCell();
                const updateButton = document.createElement("button");
                updateButton.textContent = "Update";
                updateButton.classList.add("commandBtn");
                updateButton.addEventListener("click", () => {
                    let newTitle: string = titleInput.value.trim(); // Utilisation de la valeur du champ de titre en supprimant les espaces vides
                    let newDescription: string = descriptionInput.value.trim(); // Utilisation de la valeur du champ de description en supprimant les espaces vides

                    if (confirm("are you sure you want to update this task?\n\n " + "Title: " + newTitle + " \n\n Description: " + newDescription + "")) {
                        this.editTask(task.id, newTitle, newDescription);
                    }
                });
                updateDeleteCell.appendChild(updateButton);

                const deleteButton = document.createElement("button");
                deleteButton.textContent = "Delete";
                deleteButton.classList.add("commandBtn");
                deleteButton.addEventListener("click", () => {
                    row.classList.add("delete-animation");
                    setTimeout(() => {
                        this.deleteTask(task.id);
                    }, 300); // Attend que l'animation soit terminée avant de supprimer réellement la tâche
                });
                updateDeleteCell.appendChild(deleteButton);
            });
        }
    }
    private saveNextIdToLocalStorage(): void {
        localStorage.setItem("nextid", JSON.stringify(Task.nextId));
    }
    private loadNextIdToLocalStorage(): void {
        Task.nextId = JSON.parse(localStorage.getItem("nextid") || "1");
    }

    private saveTasksToLocalStorage(): void {
        localStorage.setItem("tasks", JSON.stringify(this.tasks));
    }

    private loadTasksFromLocalStorage(): void {
        const storedTasks = localStorage.getItem("tasks");
        const storedTitle = localStorage.getItem("titleValue");
        const storedDescription = localStorage.getItem("descriptionValue");
        if (storedTasks) {
            const parsedTasks: Task[] = JSON.parse(storedTasks);
            this.tasks = parsedTasks.map((task: Task) => new Task(task.title, task.description, task.completed, task.lastModified, task.id));
        } else {
            this.tasks = [new Task("Ex: Faire la vaisselle", "Ex: Effectuer des mouvements circulaires sur la vaisselle avec une éponge", false, "2021-03-01 12:00:00", 1)];
        }
        if (storedTitle) {
            titleInput.value = storedTitle;
        }

        if (storedDescription) {
            descriptionInput.value = storedDescription;
        }
    }

    private getTaskById(id: number): Task | undefined {
        // Récupération d'une tâche en fonction de son ID
        return this.tasks.find((task) => task.id === id);
    }
}

// Création d'une instance de TaskTracker
const tracker: TaskTracker = new TaskTracker();
// Affichage des tâches au chargement de la page
tracker.showTasks();
