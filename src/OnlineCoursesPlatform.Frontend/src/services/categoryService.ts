import axiosInstance from "../api/axios";

export interface CategoryDto {
  id: number;
  categoryName: string;
}

export const getAllCategories = async (): Promise<CategoryDto[]> => {
  const response = await axiosInstance.get<CategoryDto[]>("/categories");
  return response.data;
};
