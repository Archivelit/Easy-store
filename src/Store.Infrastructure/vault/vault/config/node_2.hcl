api_addr      = "https://vault.localhost:443"
cluster_name  = "vault-second-node"
disable_mlock = true
ui            = true

storage "consul" {
  address = "ha_storage:8500"
  path    = "vault/data"
}

listener "tcp" {
  address       = "10.0.0.12:8200"
  cluster_addr  = "https://10.0.0.12:8201"
  tls_cert_file = "/vault/certificates/vault.crt"
  tls_key_file  = "/vault/certificates/vault.key"
  tls_client_ca_file = "/vault/certificates/rootCA.pem"
}

telemetry {
  prometheus_retention_time = "0"
  disable_hostname = true
  disable = true
}