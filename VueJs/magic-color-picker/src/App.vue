<script setup lang="ts">
import { ref, computed, reactive } from "vue";

interface Color {
  red: number;
  green: number;
  blue: number;
  toString(): string;
  isDuplicate?: boolean;
}

const redValue = ref(0);
const greenValue = ref(0);
const blueValue = ref(0);
const duplicateColor = ref(false);
const isDarkColor = ref(false);

const state = reactive({
  nbOfColor: 0,
  currentColor: "#000",
  savedColors: [] as Color[],
  selectedColorIndex: -1,
  isSaveDisabled: false,
});

const backgroundColor = computed(() => {
  return {
    red: redValue.value,
    green: greenValue.value,
    blue: blueValue.value,
    toString(): string {
      return `rgb(${this.red},${this.green},${this.blue})`;
    },
  };
});
function findExistingColor(color: Color): boolean {
  const existingColor = state.savedColors.find((c) => c.toString() === color.toString());

  if (existingColor) {
    existingColor.isDuplicate = true;
    setTimeout(() => {
      existingColor.isDuplicate = false;
    }, 1000);

    return true;
  }

  return false;
}

function updateColor(): void {
  const color = backgroundColor.value;

  if (findExistingColor(color)) {
    state.isSaveDisabled = true;
    duplicateColor.value = true;
    setTimeout(() => {
      duplicateColor.value = false;
    }, 1000);
  } else {
    state.isSaveDisabled = false;
    const colorExists = state.savedColors.includes(color);
    state.savedColors[state.selectedColorIndex] = color;
    state.isSaveDisabled = colorExists;
  }
  // Calcul de la luminosité de la couleur
  const brightness = (color.red * 299 + color.green * 587 + color.blue * 114) / 1000;

  // Détermine si la couleur est foncée ou claire en utilisant un seuil de luminosité
  isDarkColor.value = brightness < 128;
}

function saveColor(): void {
  if (state.selectedColorIndex === -1) {
    const color = backgroundColor.value;
    state.savedColors.push(color);
    sortColorsBySaturation();
    state.nbOfColor++;
    state.isSaveDisabled = true;
  } else {
    updateSelectedColor();
    sortColorsBySaturation();
  }
  resetColor();
}

function resetColor(): void {
  redValue.value = 0;
  greenValue.value = 0;
  blueValue.value = 0;
}
function resetApp(): void {
  state.nbOfColor = 0;
  redValue.value = 0;
  greenValue.value = 0;
  blueValue.value = 0;
  state.currentColor = "#000";
  state.savedColors = [];
  state.selectedColorIndex = -1;
  state.isSaveDisabled = false;
}

function selectColor(index: number): void {
  state.isSaveDisabled = false;
  state.selectedColorIndex = index;
  const color = state.savedColors[index];
  redValue.value = color.red;
  greenValue.value = color.green;
  blueValue.value = color.blue;
}

function updateSelectedColor(): void {
  const { selectedColorIndex } = state; // Utilisation de la déstructuration d'objet
  state.savedColors[selectedColorIndex] = backgroundColor.value;
  state.selectedColorIndex = -1;
  if (findExistingColor(backgroundColor.value)) {
    state.isSaveDisabled = true;
    duplicateColor.value = true;
    setTimeout(() => {
      duplicateColor.value = false;
    }, 1000);
  } else {
    state.isSaveDisabled = false;
  }
}
function sortColorsBySaturation(): void {
  state.savedColors.sort((colorA, colorB) => {
    const maxSaturationA = Math.max(colorA.red, colorA.green, colorA.blue);
    const maxSaturationB = Math.max(colorB.red, colorB.green, colorB.blue);

    return maxSaturationB - maxSaturationA;
  });
}

function isSelected(index: number): boolean {
  return index === state.selectedColorIndex;
}
</script>

<template>
  <header>
    <h1>Magic Color Picker</h1>
  </header>
  <section>
    <body>
      <h2>Nombre de couleurs: {{ state.nbOfColor }}</h2>
      <div class="square" :style="{ backgroundColor: backgroundColor.toString() }"></div>
      <hr />
      <div class="inputs">
        <div class="input">
          <label for="redRange">R</label>
          <input type="range" min="0" max="255" v-model="redValue" @input="updateColor" />
        </div>
        <div class="input">
          <label for="greenRange">G</label>
          <input type="range" min="0" max="255" v-model="greenValue" @input="updateColor" />
        </div>
        <div class="input">
          <label for="blueRange">B</label>
          <input type="range" min="0" max="255" v-model="blueValue" @input="updateColor" />
        </div>
      </div>
      <hr />
      <button @click="saveColor" :disabled="state.isSaveDisabled" :class="{ disabled: state.isSaveDisabled }">
        {{ state.selectedColorIndex === -1 ? "Sauvegarder" : "Modifier" }}
      </button>
      <button @click="resetApp">Reset</button>
      <hr />
      <h2 v-if="state.savedColors.length < 1" class="title">Bienvenue au Magic Color Picker</h2>
      <div v-else class="saved-square">
        <div
          @click="selectColor(index)"
          v-for="(color, index) in state.savedColors"
          :key="color.toString()"
          :class="{ 'saved-color-square': true, selected: isSelected(index), duplicateColor: color.isDuplicate }"
          :style="{ backgroundColor: color.toString() }"
        >
          <div v-if="state.selectedColorIndex === index" class="rgb">
            <span :class="isSelected(index) && isDarkColor ? 'dark-text' : ''">R: {{ color.red }}</span>
            <span :class="isSelected(index) && isDarkColor ? 'dark-text' : ''">G: {{ color.green }}</span>
            <span :class="isSelected(index) && isDarkColor ? 'dark-text' : ''">B: {{ color.blue }}</span>
          </div>
        </div>
      </div>
    </body>
  </section>
</template>

<style scoped>
.square {
  width: 100px;
  height: 100px;
  background-color: #000;
  margin: 0 auto;
  border-radius: 10px;
  box-shadow: 0 0 10px #000;
  border: #000 solid 4px;
}
.inputs {
  display: flex;
  flex-direction: column;
  width: 100%;
  align-items: center;
}
.input {
  display: flex;
  flex-direction: row;
  width: 100%;
  align-items: center;
}
input {
  width: 100%;
  margin: 0 10px;
}
.title {
  color: #000 !important;
  border-bottom: none !important;
  font-size: 1.5rem !important;
}
.saved-color-square {
  width: 60px;
  height: 60px;
  margin-right: 10px;
  border-radius: 5px;
  box-shadow: 0 0 5px rgba(0, 0, 0, 0.3);
  border: #000 solid 3px;
  margin: 5px;
  cursor: pointer;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
  flex-wrap: wrap;
}
.saved-square {
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;
  flex-wrap: wrap;
}
.selected {
  width: 70px;
  height: 70px;
  box-shadow: 0 0 10px rgb(239, 231, 8);
  cursor: auto;
}
.rgb {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
  font-size: 0.8rem;
  color: #000;
}
.rgb span {
  font-family: monospace;
  font-size: 0.9rem;
}

.disabled {
  filter: opacity(0.5);
}
.duplicateColor {
  animation: blink 0.5s linear infinite;
}

@keyframes blink {
  0% {
    opacity: 1;
  }
  50% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}
.dark-text {
  color: #fff;
}
</style>
