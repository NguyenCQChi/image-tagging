import React, { createContext, ReactNode, useState } from 'react';

interface Props {
  children: ReactNode;
}

interface LoginContextTypes {
  isLogin: boolean, 
  setIsLogin: (login: boolean) => void,
  isSent: boolean,
  setIsSent: (isSent: boolean) => void,
  isImageCaption: boolean,
  setIsImageCaption: (isCaption: boolean) => void,
  signoutFail: boolean,
  setSignoutFail: (signoutFail: boolean) => void
}

const LoginContext = createContext<LoginContextTypes>({
  isLogin: true,
  setIsLogin: (login: boolean) => {},
  isSent: true,
  setIsSent: (isSent: boolean) => {},
  isImageCaption: true,
  setIsImageCaption: (isCaption: boolean) => {},
  signoutFail: true,
  setSignoutFail: (isCaption: boolean) => {}
})

const { Provider } = LoginContext;

const LoginProvider = (props: Props) => {
  const { children } = props;
  const [ fromLogin, setFromLogin ] = useState(true);
  const [ sent, setHasSent ] = useState(false);
  const [ isImageCaption, setIsImageCaption ] = useState(true);
  const [ signoutFail, setFail ] = useState(true);

  const setIsLogin = (login: boolean) => {
    setFromLogin(login)
  }

  const setIsSent = () => {
    setHasSent(!sent)
  }

  const setHistory = () => {
    setIsImageCaption(!isImageCaption)
  }

  const setSignoutFail = (fail: boolean) => {
    setFail(fail)
  }

  return (
    <Provider
      value={{
        isLogin: fromLogin,
        setIsLogin: setIsLogin,
        isSent: sent,
        setIsSent: setIsSent,
        isImageCaption: isImageCaption,
        setIsImageCaption: setHistory,
        signoutFail: signoutFail,
        setSignoutFail: setSignoutFail
      }}
    >
      {children}
    </Provider>
  )
}

export { LoginContext, LoginProvider };