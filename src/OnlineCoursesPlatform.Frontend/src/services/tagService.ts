import axiosInstance from "../api/axios";

export interface TagDto {
  id: number;
  name: string;
  categoryId: number;
  categoryName: string;
}

export const getAllTags = async (): Promise<TagDto[]> => {
  const response = await axiosInstance.get<TagDto[]>("/tags");
  return response.data;
};
