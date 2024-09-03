module.exports = {
    serverRuntimeConfig: {
        myHost: process.env.HOST || 'localhost',
    },
    publicRuntimeConfig: {
        myHost: process.env.HOST || 'localhost',
    },
    env: {
        NEXT_PUBLIC_HOSTNAME: process.env.NEXT_PUBLIC_HOSTNAME || 'localhost',
    },
    images: {
        remotePatterns: [
            {
                protocol: 'http',
                hostname: 'www.alevelwebsite.com',
                port: '',
                pathname: '/assets/**',
            },
        ],
    },
}