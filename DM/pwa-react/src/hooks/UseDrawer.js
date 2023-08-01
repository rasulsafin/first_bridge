import { useCallback, useState } from "react";

export const useDrawer = (initialState = false) => {
  const [openDrawer, setOpenDrawer] = useState(initialState);

  const toggleDrawer = useCallback(() => setOpenDrawer(initialState => !initialState), []);

  return [openDrawer, toggleDrawer];
};