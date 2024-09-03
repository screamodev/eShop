import NextAuth, {NextAuthOptions} from "next-auth";
import IdentityServer4Provider from "next-auth/providers/identity-server4";

declare module "next-auth" {
    interface Session {
        accessToken?: string;
        idToken?: string;
        refreshToken?: string;
    }
}

declare module "next-auth/jwt" {
    interface JWT {
        accessToken?: string;
        idToken?: string;
        refreshToken?: string;
    }
}

export const authOptions: NextAuthOptions = {
    providers: [
        IdentityServer4Provider({
            id: "identity-server4",
            name: "IdentityServer4",
            type: "oauth",
            wellKnown: `http://www.alevelwebsite.com:5003/.well-known/openid-configuration`,
            authorization: { params: { scope: "openid profile mvc basket order" } },
            clientId: "mvc_pkce",
            clientSecret: "secret",
            idToken: true,
        }),
    ],
    session: {
        strategy: 'jwt',
        maxAge: 60 * 60
    },
    callbacks: {
        async jwt({ token, account, profile }) {
            if (account) {
                token.accessToken = account.access_token
            }
            return token
        },
        async session({ session, token, user }) {
            session.accessToken = token.accessToken
            return session
        }
    },
    secret: 'secret',
}

export default NextAuth(authOptions)
