<script setup lang="ts">
import { ref } from 'vue';

import { createShortenedUrl } from '../services/api/url';
import { useUrlStore } from '../stores/UrlStore';

const urlStore = useUrlStore();
const urlToShorten = ref('');

const shortenUrl = async () => {
  try {
    var createdUrl = await createShortenedUrl({
      url: urlToShorten.value,
    });

    urlToShorten.value = '';

    urlStore.addUrl(createdUrl!);
    localStorage.setItem('hashes', JSON.stringify(urlStore.urlList));
  } catch (error) {
    console.error(error);
  }
};
</script>

<template>
  <div class="w-full md:w-3/4 lg:w-1/2">
    <form @submit.prevent="shortenUrl">
      <input
        v-model="urlToShorten"
        type="url"
        placeholder="Enter a URL to shorten..."
        class="bg-secondary text-white rounded-lg h-full w-full py-2 px-6"
      />
      <button
        type="submit"
        class="bg-primary text-white rounded-lg w-full py-1 px-2 mt-2"
      >
        Shorten URL!
      </button>
    </form>
  </div>
</template>
