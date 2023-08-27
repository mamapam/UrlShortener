<script setup lang="ts">
import { onMounted } from 'vue';
import { useStorage } from '@vueuse/core';

import ShorteningForm from './components/ShorteningForm.vue';
import TheLogo from './components/TheLogo.vue';
import UrlList from './components/UrlList.vue';

import { useUrlStore } from './stores/UrlStore';
import type { IUrl } from './types';

const urlStore = useUrlStore();

onMounted(() => {
  const hashArray: IUrl[] = [];
  const hashes = useStorage('hashes', hashArray);
  hashes.value.forEach((url) => {
    urlStore.addUrl(url);
  });
});
</script>

<template>
  <main class="flex flex-col items-center justify-center m-10">
    <the-logo />
    <shortening-form />
    <url-list />
  </main>
</template>

<style>
html,
body {
  height: 100vh;
  margin: 0;
  padding: 0;
  box-sizing: border-box;
  font-family: 'Montserrat', sans-serif;
  background-color: #1c1818;
}
</style>
