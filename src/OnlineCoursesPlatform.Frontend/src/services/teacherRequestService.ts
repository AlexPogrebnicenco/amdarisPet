import axiosInstance from "../api/axios";
import type { PagedResult } from "../interfaces/PagedResult"

export interface TeacherRequestDto {
  id: number;
  userName: string;
  email: string;
  age: number;
  gender: string;
  status: string;
  requestedAt: string;
}


export const getPendingTeacherRequests = async (page: number, pageSize: number): Promise<PagedResult<TeacherRequestDto>> => {
  const response = await axiosInstance.get<PagedResult<TeacherRequestDto>>(`/teacher-requests?page=${page}&pageSize=${pageSize}`);
  return response.data;
};

export const updateTeacherRequestStatus = async (id: number, status: "Approved" | "Rejected"): Promise<void> => {
  await axiosInstance.put(`/teacher-requests/${id}`, { status });
};
