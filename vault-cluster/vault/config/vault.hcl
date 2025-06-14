api_addr                = "https://vault.localhost:8200"
cluster_addr            = "https://vault.localhost:8201"
cluster_name            = "vault-main-cluster"
disable_mlock           = true
ui                      = true

ha_storage "consul" {
  address = "ha_storage:8500"
  path    = "vault/data"
}

listener "tcp" {
  address       = "vault.localhost:8200"
  tls_cert_file = "/vault/certificates/vault.crt"
  tls_key_file  = "/vault/certificates/vault.key"
}

telemetry {
  statsite_address = "127.0.0.1:8125"
  disable_hostname = true
}