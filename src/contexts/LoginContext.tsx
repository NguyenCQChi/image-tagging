import React, { createContext, ReactNode, useState } from 'react';

interface Props {
  children: ReactNode;
}

interface LoginContextTypes {
  isLogin: boolean, 
  setIsLogin: (login: boolean) => void
}

const LoginContext = createContext<LoginContextTypes>({
  isLogin: true,
  setIsLogin: (login: boolean) => {},
})

const { Provider } = LoginContext;

const LoginProvider = (props: Props) => {
  const { children } = props;
  const [ fromLogin, setFromLogin ] = useState(true);

  const setIsLogin = (login: boolean) => {
    setFromLogin(login)
  }

  return (
    <Provider
      value={{
        isLogin: fromLogin,
        setIsLogin: setIsLogin
      }}
    >
      {children}
    </Provider>
  )
}

export { LoginContext, LoginProvider };