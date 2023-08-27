import { defineStore } from 'pinia';

import type { IUrl } from '../types';

export const useUrlStore = defineStore('urlStore', {
  state: () => {
    return {
      urlList: [] as IUrl[],
    };
  },
  getters: {
    getAllUrls: (state) => {
      return state.urlList;
    },
  },
  actions: {
    addUrl(url: IUrl) {
      this.urlList.push(url);
    },
  },
});
