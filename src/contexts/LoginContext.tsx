import React, { createContext, ReactNode, useState } from 'react';

interface Props {
  children: ReactNode;
}

interface LoginContextTypes {
  isLogin: boolean, 
  setIsLogin: (login: boolean) => void,
  isSent: boolean,
  setIsSent: (isSent: boolean) => void
}

const LoginContext = createContext<LoginContextTypes>({
  isLogin: true,
  setIsLogin: (login: boolean) => {},
  isSent: true,
  setIsSent: (isSent: boolean) => {}
})

const { Provider } = LoginContext;

const LoginProvider = (props: Props) => {
  const { children } = props;
  const [ fromLogin, setFromLogin ] = useState(true);
  const [ sent, setHasSent ] = useState(false);

  const setIsLogin = (login: boolean) => {
    setFromLogin(login)
  }

  const setIsSent = (isSent: boolean) => {
    setHasSent(isSent)
  }

  return (
    <Provider
      value={{
        isLogin: fromLogin,
        setIsLogin: setIsLogin,
        isSent: sent,
        setIsSent: setIsSent
      }}
    >
      {children}
    </Provider>
  )
}

export { LoginContext, LoginProvider };