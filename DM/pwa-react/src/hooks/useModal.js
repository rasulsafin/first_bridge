import { useCallback, useState } from "react";

export const useModal = (initialState = false) => {
  const [openModal, setOpenModal] = useState(initialState);

  const toggleModal = useCallback(() => setOpenModal(initialState => !initialState), []);

  return [openModal, toggleModal];
};