<script setup lang="ts">
import { computed } from 'vue';
import { useClipboard } from '@vueuse/core';

const props = defineProps({
  originalUrl: {
    type: String,
    required: true,
  },
  shortenedUrl: {
    type: String,
    required: true,
  },
});
const baseApiUrl = location.origin;

const fullShortUrl = computed(() => `${baseApiUrl}/${props.shortenedUrl}`);

const { copy } = useClipboard();
</script>

<template>
  <div
    class="w-full h-auto bg-secondary rounded-lg text-white py-2 px-6 mb-4 sm:text-center flex flex-col xs:text-sm md:text-base"
  >
    <p>{{ props.originalUrl }}</p>
    <p class="whitespace-nowrap">
      <span class="align-bottom font-bold">{{ fullShortUrl }}</span>
      <font-awesome-icon
        id="icon"
        icon="fa-solid fa-copy"
        class="ml-3 align-middle h-6"
        @click="copy(fullShortUrl)"
      />
    </p>
  </div>
</template>

<style scoped>
#icon {
  color: #7626d8;
  cursor: pointer;
}
#icon:hover {
  color: #8f5bcd;
}
#icon:active {
  transform: scale(0.95);
  box-shadow: 3px 2px 22px 1px rgba(0, 0, 0, 0.24);
}
</style>
