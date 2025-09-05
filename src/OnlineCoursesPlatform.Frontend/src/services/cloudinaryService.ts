import axiosInstance from "../api/axios";

export interface CloudinarySignatureResponse {
  signature: string;
  timestamp: number;
  cloudName: string;
  apiKey: string;
}

export const getCloudinarySignature = async (
  caption?: string,
  type: "video" | "image" = "video"
) => {
  const response = await axiosInstance.get<CloudinarySignatureResponse>(
    "/cloudinary/signature",
    {
      params: {
        ...(caption ? { caption } : {}),
        type,
      },
    }
  );
  return response.data;
};

const uploadToCloudinary = async (
  file: File,
  signatureData: CloudinarySignatureResponse,
  resourceType: "video" | "image",
  caption?: string
): Promise<string> => {
  const url = `https://api.cloudinary.com/v1_1/${signatureData.cloudName}/${resourceType}/upload`;

  const formData = new FormData();
  formData.append("file", file);
  formData.append("api_key", signatureData.apiKey);
  formData.append("timestamp", signatureData.timestamp.toString());
  formData.append("signature", signatureData.signature);
  if (caption) {
    formData.append("context", `caption=${caption}`);
  }

  const response = await fetch(url, {
    method: "POST",
    body: formData,
  });

  if (!response.ok) {
    throw new Error(`Failed to upload ${resourceType}`);
  }

  const data = await response.json();
  return data.secure_url;
};


export const uploadVideoToCloudinary = async (
  file: File,
  signatureData: CloudinarySignatureResponse,
  caption?: string
) => {
  return uploadToCloudinary(file, signatureData, "video", caption);
};

export const uploadImageToCloudinary = async (
  file: File,
  signatureData: CloudinarySignatureResponse,
  caption?: string
) => {
  return uploadToCloudinary(file, signatureData, "image", caption);
};


export const deleteCloudinaryVideo = async (publicId: string) => {
  const response = await axiosInstance.delete("/cloudinary/delete", {
    params: { publicId },
  });
  return response.data;
};

export const deleteCloudinaryImage = async (publicId: string) => {
  const response = await axiosInstance.delete("/cloudinary/delete-image", {
    params: { publicId },
  });
  return response.data;
};