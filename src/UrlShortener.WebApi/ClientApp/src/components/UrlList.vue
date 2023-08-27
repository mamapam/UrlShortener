<script setup lang="ts">
import { useUrlStore } from '../stores/UrlStore';
import UrlItem from './UrlItem.vue';

const urlStore = useUrlStore();
const hashes = urlStore.urlList;
</script>

<template>
  <div v-if="hashes.length !== 0" class="w-full md:max-w-200 mt-8">
    <url-item
      v-for="url in hashes"
      :key="url.id"
      :original-url="url.originalUrl"
      :shortened-url="url.urlHash"
    ></url-item>
  </div>
  <div v-else class="text-white mt-6">Try shortening a URL!</div>
</template>
