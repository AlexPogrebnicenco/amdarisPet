import axiosInstance from "../api/axios";

import type {
  CourseDetails,
  PublicCourse,
  TeacherOwnCourse,
  CreateCourseRequest
} from "../interfaces/CourseDto";
import type { JsonPatchOperation } from "../interfaces/JsonPatchOperation";
import type { PagedResult } from "../interfaces/PagedResult";

interface GetTeacherOwnCoursesRequest {
  page: number;
  pageSize: number;
  sort?: string;
  tag?: string | null;
  search?: string;
}

export const createCourse = async (data: CreateCourseRequest) => {
  const response = await axiosInstance.post("/courses", data);
  return response.data;
};

export const deleteCourse = async (courseId: number): Promise<void> => {
  await axiosInstance.delete(`/courses/${courseId}`);
};

export const patchCourse = async (
  id: number,
  patch: JsonPatchOperation[]
): Promise<void> => {
  await axiosInstance.patch(`/courses/${id}`, patch);
};



export const getTeacherOwnCourses = async ({
  page,
  pageSize,
  sort,
  tag,
  search,
}: GetTeacherOwnCoursesRequest): Promise<PagedResult<TeacherOwnCourse>> => {
  const response = await axiosInstance.get("/courses/teacher/own-courses", {
    params: {
      pageNumber: page, // 👈 фикс
      pageSize,
      sort,
      tag,
      search,
    },
  });
  return response.data;
};

export const getCourseById = async (id: number): Promise<CourseDetails> => {
  const response = await axiosInstance.get<CourseDetails>(`/courses/${id}`);
  return response.data;
};

export const getLastCreatedCourses = async (
  pageNumber = 1,
  pageSize = 10
): Promise<PublicCourse[]> => {
  const response = await axiosInstance.get<PublicCourse[]>("/courses/last-created", {
    params: { pageNumber, pageSize },
  });
  return response.data;
};

const sortToPath: Record<string, string> = {
  lastCreated: "last-created",
  mostPopular: "most-popular",
  lastModified: "last-modified",
  longest: "longest",
  shortest: "shortest",
};

export const getSortedCourses = async (
  sortType: string,
  pageNumber = 1,
  pageSize = 10
): Promise<PublicCourse[]> => {
  const path = sortToPath[sortType];
  if (!path) {
    throw new Error(`Invalid sortType: ${sortType}`);
  }

  const response = await axiosInstance.get<PublicCourse[]>(`/courses/${path}`, {
    params: { pageNumber, pageSize },
  });
  return response.data;
};

export const getCoursesByTags = async (
  tags: string[],
  pageNumber = 1,
  pageSize = 10
): Promise<PublicCourse[]> => {
  const response = await axiosInstance.get<PublicCourse[]>("/courses/by-tags", {
    params: { tags, pageNumber, pageSize },
    paramsSerializer: (params) => {
      const searchParams = new URLSearchParams();
      params.tags.forEach((tag: string) => searchParams.append("tags", tag));
      searchParams.append("pageNumber", params.pageNumber.toString());
      searchParams.append("pageSize", params.pageSize.toString());
      return searchParams.toString();
    },
  });

  return response.data;
};

export const searchCoursesByTitle = async (
  query: string,
  pageNumber = 1,
  pageSize = 10
): Promise<PublicCourse[]> => {
  const response = await axiosInstance.get<PublicCourse[]>("/courses/search", {
    params: { query, pageNumber, pageSize },
  });

  return response.data;
};



