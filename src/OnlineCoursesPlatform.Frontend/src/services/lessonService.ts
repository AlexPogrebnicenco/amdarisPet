import axiosInstance from "../api/axios";
import type { Operation } from "fast-json-patch";

export interface VideoInfo {
  url: string;
  title: string;
}

export interface CreateLessonRequest {
  title: string;
  description: string;
  orderNumber: number;
  content?: string | null; 
  resources?: string[]; 
  videoUrls?: VideoInfo[];
}

export interface Lesson {
  id: number;
  title: string;
  description: string;
  orderNumber: number;
  courseId: number;
  content?: string;
  resources?: string[];
  videoUrls?: VideoInfo[];
}

export interface LessonList {
  id: number;
  title: string;
  orderNumber: number;
  courseId: number;
}


export const createLesson = async (courseId: number, data: CreateLessonRequest) => {
  const response = await axiosInstance.post(`/courses/${courseId}/lessons`, data);
  return response.data;
};

export const getLessonsByCourseId = async (courseId: number): Promise<LessonList[]> => {
  const response = await axiosInstance.get(`/courses/${courseId}/lessons`);
  return response.data;
};
export const getLessonByOrderNumber = async (courseId: number, orderNumber: number): Promise<Lesson> => {
  const response = await axiosInstance.get(`/courses/${courseId}/lessons/${orderNumber}`);
  return response.data;
};

export const deleteLesson = async (courseId: number, orderNumber: number) => {
  await axiosInstance.delete(`/courses/${courseId}/lessons/${orderNumber}`);
};

export const updateLesson = async (
  courseId: number,
  orderNumber: number,
  patch: Operation[]
) => {
  await axiosInstance.patch(
    `/courses/${courseId}/lessons/${orderNumber}`,
    patch,
    {
      headers: {
        "Content-Type": "application/json-patch+json",
      },
    }
  );
};
