import { createApp } from 'vue';
import { createPinia } from 'pinia';

import { library } from '@fortawesome/fontawesome-svg-core';
import { faCopy } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

import App from './App.vue';

import './assets/styles/tailwind.css';

const app = createApp(App);
const pinia = createPinia();

library.add(faCopy);
app.component('FontAwesomeIcon', FontAwesomeIcon);

app.use(pinia);

app.mount('#app');
