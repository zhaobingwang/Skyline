const ENV = {
  dev: {
    apiUrl: 'http://localhost:44349',
    oAuthConfig: {
      issuer: 'http://localhost:44349',
      clientId: 'Skyline_App',
      clientSecret: '1q2w3e*',
      scope: 'Skyline',
    },
    localization: {
      defaultResourceName: 'Skyline',
    },
  },
  prod: {
    apiUrl: 'http://localhost:44349',
    oAuthConfig: {
      issuer: 'http://localhost:44349',
      clientId: 'Skyline_App',
      clientSecret: '1q2w3e*',
      scope: 'Skyline',
    },
    localization: {
      defaultResourceName: 'Skyline',
    },
  },
};

export const getEnvVars = () => {
  // eslint-disable-next-line no-undef
  return __DEV__ ? ENV.dev : ENV.prod;
};
