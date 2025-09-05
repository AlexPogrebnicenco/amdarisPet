import axiosInstance from "../api/axios";

export const enrollInCourse = async (courseId: number): Promise<void> => {
  await axiosInstance.post(`/courses/${courseId}/enroll`);
};

export const unenrollFromCourse = async (courseId: number): Promise<void> => {
  await axiosInstance.delete(`/courses/${courseId}/unenroll`);
};

export const isEnrolledInCourse = async (courseId: number): Promise<boolean> => {
  const response = await axiosInstance.get(`/courses/${courseId}/is-enrolled`);
  return response.data.isEnrolled;
};

export const getEnrollmentCount = async (courseId: number): Promise<number> => {
  const response = await axiosInstance.get(`/courses/${courseId}/enrollments-count`);
  return response.data.enrollmentCount;
};
