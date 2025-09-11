FROM quay.io/keycloak/keycloak:26.3.3

COPY realm-export.json /tmp/realm.json

ENTRYPOINT ["/opt/keycloak/bin/kc.sh", "start-dev"]