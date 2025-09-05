import axiosInstance from "../api/axios";
import type { PublicCourse } from "../interfaces/CourseDto";
import type { PagedResult } from "../interfaces/PagedResult";

export interface UserAccount {
  userName: string;
  avatarUrl: string | null;
}

export interface UpdateUserDto {
  userName?: string;
  avatarUrl?: string;
}


interface GetEnrolledCoursesRequest {
  page: number;
  pageSize: number;
  sort?: string;
  tag?: string | null;
  search?: string;
}

// Получить текущую информацию о пользователе
// export const getUserAccount = async (): Promise<UserAccount> => {
//   const response = await axiosInstance.get<UserAccount>("/users/account");
//   return response.data;
// };

// Обновить информацию о пользователе
export const updateUserAccount = async (data: UpdateUserDto): Promise<UserAccount> => {
  const response = await axiosInstance.put<UserAccount>("/users/account", data);
  return response.data;
};

export const getUserEnrolledCourses = async ({
  page,
  pageSize,
  sort,
  tag,
  search,
}: GetEnrolledCoursesRequest): Promise<PagedResult<PublicCourse>> => {
  const response = await axiosInstance.get<PagedResult<PublicCourse>>("/users/me/enrollments", {
    params: {
      pageNumber: page,
      pageSize,
      sort,
      tag,
      search,
    },
  });
  return response.data;
};
