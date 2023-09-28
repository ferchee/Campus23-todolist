prereq:
 docker hub: webapp push-olva

oc apply 

oc annotate service testappservice --overwrite service.beta.openshift.io/serving-cert-secret-name=testappservice

backend db szktipt futtatás: 
oc rsh , cd /usr/share/container-scripts/postgresql/postgres-init

oc scale
