-- public."rule" definition

-- Drop table

-- DROP TABLE public."rule";

CREATE TABLE public."rule" (
	rul_id int4 NOT NULL DEFAULT nextval('"Rules_rul_id_seq"'::regclass),
	rul_name varchar(20) NULL,
	rul_desc varchar(50) NULL,
	rul_condition varchar(100) NULL,
	rul_type int4 NOT NULL,
	rul_is_active bool NOT NULL,
	rul_created_by varchar(50) NULL,
	rul_modified_by varchar(50) NULL,
	rul_is_deleted bool NULL DEFAULT true,
	rul_is_used bool NOT NULL,
	rul_created timestamp NOT NULL DEFAULT now(),
	rul_modified varchar(50) NULL,
	rul_output_type varchar(50) NULL,
	rul_approved_status varchar(50) NULL,
	rul_approved_by varchar(50) NULL,
	rul_category varchar(50) NULL,
	rul_id_ori varchar(50) NULL,
	rul_version varchar(50) NULL,
	rul_applied varchar(50) NULL,
	rul_output varchar(20) NULL,
	CONSTRAINT "PK_rule" PRIMARY KEY (rul_id)
);

-- public.rule_final definition

-- Drop table

-- DROP TABLE public.rule_final;

CREATE TABLE public.rule_final (
	rul_id serial4 NOT NULL,
	rul_name varchar(20) NULL,
	rul_desc varchar(50) NULL,
	rul_condition varchar(100) NULL,
	rul_output varchar(20) NULL,
	rul_type int4 NOT NULL,
	rul_is_active bool NOT NULL,
	rul_created_by varchar(50) NULL,
	rul_modified_by varchar(50) NULL,
	rul_is_deleted bool NULL DEFAULT false,
	rul_is_used bool NOT NULL,
	rul_created timestamp NOT NULL DEFAULT now(),
	rul_modified varchar(50) NULL,
	rul_output_type varchar(50) NULL,
	rul_approved_status varchar(50) NULL,
	rul_approved_by varchar(50) NULL,
	rul_category varchar(50) NULL,
	rul_id_ori varchar(20) NOT NULL,
	rul_version varchar(20) NULL,
	rul_applied varchar(50) NULL,
	CONSTRAINT "PK_rule_final" PRIMARY KEY (rul_id)
);
-- "version".parameter_version definition

-- Drop table

-- DROP TABLE "version".parameter_version;

CREATE TABLE "version".parameter_version (
	prv_id int4 NOT NULL DEFAULT nextval('version."ParameterVersions_prv_id_seq"'::regclass),
	prv_module varchar(20) NULL,
	prv_version varchar(50) NULL,
	prv_date timestamp NOT NULL DEFAULT now(),
	prv_unique_parameter int4 NULL,
	prv_sync_plan timestamp NULL,
	prv_headerid int4 NULL,
	prv_status varchar(10) NULL,
	CONSTRAINT "PK_parameter_version" PRIMARY KEY (prv_id)
);

CREATE OR REPLACE FUNCTION public.ins_parameter_version(_module character varying, _version character varying, _status character varying, _unique_parameter integer, _headerid integer, _date character varying, _sync_plan character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
	declare
    row_count integer;
   	new_id integer;
	begin
		WITH rows AS (
		   INSERT INTO "version".parameter_version (prv_module, prv_version
		   	, prv_status, prv_unique_parameter, prv_headerid
		   	--, prv_date, prv_sync_plan
		   )
		   VALUES(_module, _version
				, _status, _unique_parameter, _headerid
				--, _date, _sync_plan
		 	) returning prv_id
		) SELECT max(prv_id) into new_id FROM rows;	 
		
		return new_id;
	END;
$function$
;
