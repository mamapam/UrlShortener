import axios from 'axios';
import type { ICreateShortenedUrl, IUrl } from '@/types';

export const createShortenedUrl = async (body: ICreateShortenedUrl) => {
  try {
    const { data } = await axios.post<IUrl>('/url', body);
    return data;
  } catch (error) {
    console.log(error);
  }
};
